using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameUI : MonoBehaviour
{
    [Header("Game Components")]
    public GameObject canvas;
    public RectTransform fxHolder;
    public RectTransform startHolder;
    public RectTransform finishHolder;
    public Image circleImage;
    public TMP_Text textScore;
    public GameObject startContainer;
    public GameObject badgeHolderPrefab;
    public RectTransform badgePos;

    [Header("Particles")]
    public ParticleSystem combiParticles;
    public ParticleSystem missParticles;
    public GameObject cursorEffect1;
    public GameObject cursorEffect2;
    public ParticleSystem scoreEffect1;
    public ParticleSystem scoreEffect2;
    public ParticleSystem scoreEffect3;
    public ParticleSystem scoreEffect4;
    public ParticleSystem sparkleEffect;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip comboClip;
    public float pitch = 1f;

    private float defaultSpeed = 50f; // start speed
    private float speed = 50f; // speed of rotation - changed during game
    private float hitThreshold = 2.5f;
    private float moveThreshold = 2.5f;
    private float progressFill = 0f;
    private float progressRotation = 0f;
    private float progressVirtual = 0f;
    private float startVirtual = 0f;
    private float finishVirtual = 100f;
    private int direction = 1;
    private int score = 0;
    private int comboCount = 0;
    private bool hasStarted = false;
    private bool isClickInProgress = false;
    private UserData userData;
    private float progress = 0f;

    void OnMouseDown()
    {
        Debug.Log("mouse down");

        if (hasStarted)
        {
            onClick();
        } else
        {
            pitch = 0.8f;
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(comboClip);
            pitch = 1f;
            hasStarted = true;
            startContainer.SetActive(false);
        }

        
    }

    private void Start()
    {
        userData = SaveController.Load();

        if (userData == null)
        {
            //Debug.Log("UserData is null");
            userData = new UserData();
            SaveController.Save(userData);
        }
    }

    void Update()
    {
        var target = startVirtual;
        if (direction == 1)
        {
            target = finishVirtual;
        }

        var weighting = ((finishVirtual - startVirtual) / 100) * (speed * 0.33f);
        var weightedSpeed = (speed * 0.66f) + weighting;

        if (hasStarted && !isClickInProgress)
        {
            if (isPastTarget(target, progressVirtual, direction))
            {
                EndGame();
            }

            progress = progress + (weightedSpeed * Time.deltaTime);

            if (progress > 100)
            {
                progress = 0f;
            }
            if (progress < 0)
            {
                progress = 100f;
            }

            progressFill = progress / 100;
            circleImage.fillAmount = progressFill;

            if (direction == 1)
            {
                circleImage.fillClockwise = true;
            }
            else
            {
                circleImage.fillClockwise = false;
            }

            progressRotation = progressRotation + (weightedSpeed * direction * Time.deltaTime);  // progress with direction applied

            if (progressRotation > 100)
            {
                progressRotation = 100f;
            }
            if (progressRotation < 0)
            {
                progressRotation = 0f;
            }

            progressVirtual = progressVirtual + (weightedSpeed * direction * Time.deltaTime);
            if ((direction == 1 && progressVirtual > 100) || (direction == -1 && progressVirtual < 0))
            {
                EndGame();
            }

            fxHolder.rotation = Quaternion.Euler(new Vector3(0f, 0f, -(progressRotation / 100) * 360));

            textScore.text = score.ToString();
        }
    }

    public void onClick()
    {
        isClickInProgress = true;

        var target = startVirtual;
        if (direction == 1)
        {
            //Debug.Log("target is finish");
            target = finishVirtual;
        } else
        {
            //Debug.Log("target is start");
        }

        var actualRotation = fxHolder.rotation.eulerAngles.z;

        // Is bar close enough to be treated as a perfect hit on line but not past
        if (isWithinThreshold(target, progressVirtual, hitThreshold))
        {
            progressVirtual = target;
            progressRotation = target;
            fxHolder.rotation = Quaternion.Euler(new Vector3(0f, 0f, -(progressRotation / 100) * 360));

            audioSource.PlayOneShot(comboClip);
            pitch = pitch + 0.05f;
            if (pitch > 2.6) pitch = 2.6f;
            audioSource.pitch = pitch;

            combiParticles.Play();
            
            comboCount++;

            if (comboCount >= 3)
            {
                //Debug.Log("combo x3 + : " + progressRotation);

                // increase size

                progressRotation = progressRotation + (moveThreshold * direction);
                progressRotation = checkWithinBounds(progressRotation);
                //Debug.Log($"Combo new progressRotation: {progressRotation}");

                fxHolder.rotation = Quaternion.Euler(new Vector3(0f, 0f, -(progressRotation / 100) * 360));

                // Also update progress virtual and 'target' virtual as new positions needs to be reflected by target markers
                progressVirtual = progressVirtual + (moveThreshold * direction);
                progressVirtual = checkWithinBounds(progressVirtual);
                //Debug.Log($"Combo new progressVirtual: {progressVirtual}");

                updateTargetRotation();
            }

            setValuesToContinuePlay();
            speed = speed + 5;
        }
        else  // not close so detect other rules
        {
            // Has gone past the target
            if (isPastTarget(target, progressVirtual, direction))
            {
                EndGame();

            } else
            {
                pitch = 1f;
                audioSource.pitch = pitch;
                audioSource.PlayOneShot(comboClip);

                // Move FX Holder in by a small amount if near target to move game on quicker
                if (isWithinThreshold(target, progressVirtual, moveThreshold))
                {
                    // move progressRotation as fxHolder and other tranforms use this value
                    progressRotation = progressRotation - (moveThreshold * direction);
                    progressRotation = checkWithinBounds(progressRotation);
                    //Debug.Log($"New progressRotation: {progressRotation}");

                    fxHolder.rotation = Quaternion.Euler(new Vector3(0f, 0f, -(progressRotation / 100) * 360));

                    // Also update progress virtual and 'target' virtual as new positions needs to be reflected by target markers
                    progressVirtual = progressVirtual - (moveThreshold * direction);
                    progressVirtual = checkWithinBounds(progressVirtual);
                    //Debug.Log($"New progressVirtual: {progressVirtual}");
                }

                // Miss
                missParticles.Play();

                updateTargetRotation();

                setValuesToContinuePlay();
                speed = defaultSpeed;
                audioSource.pitch = pitch;
                comboCount = 0;
            }
        }

        ShowEffects();
        CalculateBadges();
    }

    private void ShowEffects()
    {
        if (score > 5)
        {
            cursorEffect1.SetActive(true);
        }

        if (score > 10)
        {
            scoreEffect1.Play();
        }

        if (score > 20)
        {
            scoreEffect2.Play();
        }

        if (score > 25)
        {
            cursorEffect1.SetActive(false);
            cursorEffect2.SetActive(true);
        }

        if (score > 30)
        {
            scoreEffect3.Play();
        }

        if (score > 40)
        {
            scoreEffect4.Play();
        }

        if (score > 50)
        {
            sparkleEffect.Play();
        }
    }

    private void CalculateBadges()
    {
        var allBadges = new List<Enums.Badges>();
        var scoreBadges = BadgeController.CalculateScoreBadges(userData, score);
        foreach (var badge in scoreBadges)
        {
            userData.Badges.Add(badge, true);
            allBadges.Add(badge);
        }

        var comboBadges = BadgeController.CalculateComboBadges(userData, comboCount);
        foreach (var badge in comboBadges)
        {
            userData.Badges.Add(badge, true);
            allBadges.Add(badge);
        }

        var activityBadges = BadgeController.CalculateActivityBadges(userData);
        foreach (var badge in activityBadges)
        {
            userData.Badges.Add(badge, true);
            allBadges.Add(badge);
        }

        // Save user
        SaveController.Save(userData);

        ShowBadges(allBadges);
    }

    private void ShowBadges(List<Enums.Badges> badges)
    {
        if (badges.Count > 0)
        {
            // Get first badge in list
            var badge = badges[0];

            // display badge to user
            GameObject badgeHolder = Instantiate(badgeHolderPrefab, badgePos.position, badgePos.rotation, canvas.transform);
            GameObject badgeTextObject = badgeHolder.transform.GetChild(1).gameObject;

            var badgeText = badgeTextObject.GetComponent<TMP_Text>();
            var text = BadgeController.GetBadgeText(badge);
            badgeText.text = text;
            Animator animator = badgeHolder.GetComponent<Animator>();
            animator.SetTrigger("BadgeCreated");

            // remove badge from list
            badges.RemoveAt(0);

            // call destroy function coroutine to destroy in 1.5f and callback
            Destroy(badgeHolder, 1.5f);

            // Run delayed recursive call to get next badge
            StartCoroutine(DelayedShowBadgesCallback(badges));
        }
    }

    IEnumerator DelayedShowBadgesCallback(List<Enums.Badges> badges)
    {
        yield return new WaitForSeconds(1.5f);
        ShowBadges(badges);
        
    }

    private bool isWithinThreshold(float target, float actual, float threshold)
    {
        float min;
        float max;

        //Debug.Log($"isWithinThreshold: target: {target}, actual: {actual}, threshold: {threshold}");

        if (direction == 1)
        {
            min = target - threshold;
            max = target;
        } else
        {
            min = target;
            max = target + threshold;
        }

        if (actual > min && actual < max)
        {
            //Debug.Log($"isWithinThreshold: true");
            return true;
        }
        //Debug.Log($"isWithinThreshold: false");
        return false;
    }

    private bool isPastTarget(float target, float actual, int direction)
    {
        if (direction == 1 && actual > target || direction == -1 && actual < target)
        {
            return true;
        }
        return false;
    }

    private void setValuesToContinuePlay()
    {
        //Debug.Log($"setValuesToContinuePlay");
        circleImage.transform.rotation = fxHolder.rotation;
        progress = 0f;
        direction = direction * -1;
        score++;
        isClickInProgress = false;
    }

    private void updateTargetRotation()
    {
        //Debug.Log($"updateTargetRotation: direction: {direction}");
        if (direction == 1)
        {
            finishVirtual = progressVirtual;
            //Debug.Log($"New finish: {finishVirtual}");
            finishHolder.rotation = fxHolder.rotation;
        }
        else
        {
            startVirtual = progressVirtual;
            //Debug.Log($"New start: {startVirtual}");
            startHolder.rotation = fxHolder.rotation;
        }
    }

    private float checkWithinBounds(float check)
    {
        if (check > 100)
        {
            //Debug.Log("reset > 100");
            check = 100f;
        }
        if (check < 0)
        {
            //Debug.Log("reset < 0");
            check = 0f;
        }
        return check;
    }

    private void EndGame()
    {        
        if (userData.HighScore < score)
        {
            userData.HighScore = score;
        }

        userData.FailCount = userData.FailCount + 1;

        // calculate fail badges and add to userData before scene reload as won't be calculated by click function above.
        var activityBadges = BadgeController.CalculateActivityBadges(userData);
        foreach (var badge in activityBadges)
        {
            userData.Badges.Add(badge, true);
        }

        SaveController.Save(userData);

        // END GAME - SHOW CURRENT SCORE
        LevelController.Instance.PlayGame();
    }
}
