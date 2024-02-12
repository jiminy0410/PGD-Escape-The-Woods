using UnityEngine;

public class DoorSystems : DoorComponents, IInteractable
{

    //public AnalyticsSystem analSys;
    [SerializeField] private AudioSource teleportSFX;
    private PersistingSounds persistingSounds;

    public void Start()
    {

        player = GameObject.Find("Player");
        Debug.Log(player.name);

        cooldownCount = Time.time + cooldownTime;

        levelManager = GameObject.Find("LevelManager");

        //analSys = GameObject.Find("AnalyticsObject").GetComponent<AnalyticsSystem>();
        persistingSounds = GameObject.Find("PersistingSounds").GetComponent<PersistingSounds>();
    }


    public void Interact()
    {
        if (Time.time > cooldownCount)
        {

            if (!finalDoor)
            {
                if (nextDoor != null)
                {
                    player.transform.position = nextDoor.transform.Find("TransportPoint").position;
                    cooldownCount = Time.time + cooldownTime;
                    nextDoor.gameObject.GetComponent<DoorSystems>().cooldownCount = this.cooldownCount;

                    teleportSFX.pitch = Random.RandomRange(0.7f, 1.0f);
                    teleportSFX.Play();

                    GameObject deathPit = GameObject.Find("DeathPit");
                }
                else
                {
                    //analSys.timePlayedThisLevel += (int)Time.time - analSys.levelStartTime;

                    //load the next scene.
                    //GameObject.Find("AnalyticsObject").GetComponent<AnalyticsSystem>().SendLevelCompletionEvent();

                    //analSys.resetVariables();

                    SaveSystem.EraseData();

                    levelManager.GetComponent<LevelManager>().LoadNextLevel();
                    persistingSounds.PlaySound("levelComplete");
                }
            }
            else
            {
                persistingSounds.PlaySound("levelComplete");

                //analSys.timePlayedThisLevel += (int) Time.time - analSys.levelStartTime;

                //NOT THE FINAL RESULT, CHANGE THIS TO THE VICTORY SCREEN OR SOMETHING
                //analSys.SendLevelCompletionEvent();

                //analSys.resetVariables();

                SaveSystem.EraseData();
                Application.Quit();
            }
        }
    }
}
