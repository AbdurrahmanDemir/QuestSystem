using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestTracker : MonoBehaviour
{
    private QuestManager questManager;
    private void Awake()
    {
        questManager = GetComponent<QuestManager>();

        menuManager.onAdsWatch += AdsWatchCallback;
        upgradeSystem.onTowerUpgrade+= TowerUpgradeCallback;
        cardManager.onOpenCards += OpenCardsCallback;
    }
    private void Start()
    {
        /*Enemy Kill ve Arena tamamlama g�revlerini sava� sahnesinin bulundu�u sciptlerden �a��rmam�z gerekiyordu.
        Dont destroy load gibi sava� sahnelerindeki sciptlerinden almak istedim ama �al��mad�. Bu y�zden �ld�r�len enemy say�s�n� ve
        tamamlanan arena say�s�n� sava� sahnelerinde PlayerPrefs ile kaydediyorum burda �a��r�yorum.*/

        EnemyDiedCallback();
        ArenaLevelCallback();


    }
    private void OnDestroy()
    {
        menuManager.onAdsWatch -= AdsWatchCallback;
        upgradeSystem.onTowerUpgrade -= TowerUpgradeCallback;
        cardManager.onOpenCards -= OpenCardsCallback;
    }

    private void EnemyDiedCallback()
    {
        Dictionary<int, Quest> quests = new Dictionary<int, Quest>(questManager.GetCurrentQuest()); //aktif g�revi al�yoruz

        foreach (KeyValuePair<int, Quest> questData in quests)
        {
            Quest quest = questData.Value;

            if (quest.Type == QuestType.Kill)
            {
                int currentEnemiesKilled = (int)(quest.progress * quest.target);
                int totalKill= PlayerPrefs.GetInt("totalKill");
                //currentEnemiesKilled++;
                currentEnemiesKilled = totalKill;
                float newProgress = (float)currentEnemiesKilled / quest.target;

                questManager.UpdateQuestProgress(questData.Key, newProgress);
            }
        }
    }

    private void AdsWatchCallback()
    {
        Dictionary<int, Quest> quests = new Dictionary<int, Quest>(questManager.GetCurrentQuest());

        foreach (KeyValuePair<int, Quest> questData in quests)
        {
            Quest quest = questData.Value;

            if (quest.Type == QuestType.AdsWatch)
            {
                int currentAdsWatch = (int)(quest.progress * quest.target);
                currentAdsWatch++;

                float newProgress = (float)currentAdsWatch / quest.target;

                questManager.UpdateQuestProgress(questData.Key, newProgress);
            }
        }
    }

    private void TowerUpgradeCallback()
    {
        Dictionary<int, Quest> quests = new Dictionary<int, Quest>(questManager.GetCurrentQuest());

        foreach (KeyValuePair<int, Quest> questData in quests)
        {
            Quest quest = questData.Value;

            if (quest.Type == QuestType.TowerUpgrade)
            {
                int currentTowerLevel = (int)(quest.progress * quest.target);
                currentTowerLevel++;

                float newProgress = (float)currentTowerLevel / quest.target;

                questManager.UpdateQuestProgress(questData.Key, newProgress);
            }
        }
    }

    private void OpenCardsCallback()
    {
        Dictionary<int, Quest> quests = new Dictionary<int, Quest>(questManager.GetCurrentQuest());

        foreach (KeyValuePair<int, Quest> questData in quests)
        {
            Quest quest = questData.Value;

            if (quest.Type == QuestType.OpenCard)
            {
                int currentCard = (int)(quest.progress * quest.target);
                currentCard++;

                float newProgress = (float)currentCard / quest.target;

                questManager.UpdateQuestProgress(questData.Key, newProgress);
            }
        }
    }

    private void ArenaLevelCallback()
    {
        Dictionary<int, Quest> quests = new Dictionary<int, Quest>(questManager.GetCurrentQuest());

        foreach (KeyValuePair<int, Quest> questData in quests)
        {
            Quest quest = questData.Value;

            if (quest.Type == QuestType.ArenaLevel)
            {
                int currentArenaLevel = (int)(quest.progress * quest.target);
                int completedArena = PlayerPrefs.GetInt("compeletedArenaQuest");
                //currentArenaLevel++;
                currentArenaLevel = completedArena;
                float newProgress = (float)currentArenaLevel / quest.target;

                questManager.UpdateQuestProgress(questData.Key, newProgress);
            }
        }
    }
}
