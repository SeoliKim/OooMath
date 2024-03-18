using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberRound_Achievement : Achievement {
    protected override void Awake() {
        type = 0;
        index = GetIndex();
        UpdateAchievement();
    }

    protected int GetIndex() {
        Transform parent = transform.parent;
        List<NumberRound_Achievement> calculationrecords = new List<NumberRound_Achievement>();
        foreach (Transform child in parent) {
            if (child.gameObject.GetComponent<NumberRound_Achievement>() != null) {
                calculationrecords.Add(child.gameObject.GetComponent<NumberRound_Achievement> ());
            }
        }
        for (int i = 0; i < calculationrecords.Count; i++) {
            if (calculationrecords[i] == this) {
                return i;
            }
        }
        Debug.Log("Calculation_Achievement returns no valid index in gameobject " + gameObject.name);
        return -1;
    }

    protected override float GetUserData() {
        float calcQuestion = User.I.GetCalcQuestion(index);
        return calcQuestion;
    }

    protected override int GetMilstoneCount() {
        int milestoneCount = 7;
        return milestoneCount;
    }

    protected override float Goal(int mileStone) {
        if (mileStone == 0) {
            return 0;
        }
        if (mileStone == 1) {
            return 20;
        }
        if (mileStone == 3) {
            return 50;
        }
        if (mileStone == 4) {
            return 120;
        }
        if (mileStone == 5) {
            return 180;
        }
        if (mileStone == 6) {
            return 240;
        }
        if (mileStone == 7) {
            return 300;
        }
        return -1;
    }

    protected override int GetRewardCPAmount(int rewardMilestone) {
        if (rewardMilestone == 0) {
            return 0;
        }
        if (rewardMilestone == 1) {
            return 100;
        }
        if (rewardMilestone == 3) {
            return 50;
        }
        if (rewardMilestone == 4) {
            return 100;
        }
        if (rewardMilestone == 5) {
            return 200;
        }
        if (rewardMilestone == 6) {
            return 300;
        }
        if (rewardMilestone == 7) {
            return 300;
        }
        return -1;
    }

    protected override int GetRewardSkinIndex(int rewardMilestone) {//override
        if (rewardMilestone == 3) {
            return 0;
        }
        if (rewardMilestone == 4) {
            return 1;
        }
        if (rewardMilestone == 7) {
            return 2;
        }
        return -1;
    }
}
