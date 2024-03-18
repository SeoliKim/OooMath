using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UserData : MonoBehaviour {}
public class ExperienceInfo {
    public int experience;
    public int lv;
    public int totalCalcGame;
    public int totalNumGame;

    //preference
    public float musicVolume;
    public float soundEffectVolume;
    public ExperienceInfo() {
        experience = 0;
        lv=1;
        totalCalcGame = 0;
        totalNumGame=0;
        musicVolume = .5f;
        soundEffectVolume = .8f;
    }

}
public class GameRecord {
    public int[] calcQuestion;
    public int[] numMonsterRound;
    public int[] numMonsterKillMax;

    public int[,] achievementRewards;
    /*
     * 0-calculation 
     * 0- total Game Count
     * 2- 
     */

    public GameRecord() {
        calcQuestion = new int[12];
        numMonsterRound = new int[12];
        numMonsterKillMax = new int[12];

        achievementRewards = new int[20, 20];
    }

    public void Update(GameRecord gameRecord) {
        if (gameRecord.calcQuestion == null) {
            gameRecord.calcQuestion = new int[12];
        }
        if (gameRecord.numMonsterRound == null) {
            gameRecord.numMonsterRound = new int[12];
        }
        if (gameRecord.numMonsterKillMax == null) {
            gameRecord.numMonsterKillMax = new int[12];
        }
        if (gameRecord.achievementRewards == null) {
            gameRecord.achievementRewards = new int[20, 20];
        }

    }

}

public class MissionRecord {
    public bool[] tutorial;

    /*
     * Tutorial
     * 0- Welcome Introduction, Basic Move
     * 1- Calculation Game Basic
     * 2- 
     */

    public bool[] goals;
    /*
     * Goal
     * 0- Tutorial 0
     * 1- Tutorial 1
     * 2- 
     */

    public bool[,] OwnSkins;
    /*
     * 0- Basic:
     * 0,0-Default White;
     * 0,1-Red;
     * 0,2-Yellow
     * 0,3-Green
     * 0,4-Blue
     * 0,5-Orange
     * 0,6-Pink
     * 0,7-purple
     * 0,8-grey
     * 0,9-skin
     * 1,0-black
     * 1,1- 
     * 1,2- 
     * 
     * 1- Royal:
     * 1- Tutorial 1
     * 
     * 2- Award:
     * 2,0- calc050- whilte-blue half
     * 2,1- calc0120- white-blue line
     * 2,2- calc0300
     * 2,3- calc150
     * 2,4- calc0
     */

    public int onSkinCollection;
    public int onSkinIndex;


    public MissionRecord() {
        tutorial = new bool[100];
        goals = new bool[100];
        OwnSkins = new bool[3,100];
        onSkinCollection = 0;
        onSkinIndex = 0;
        OwnSkins[onSkinCollection, onSkinIndex] = true;
    }
}

public class DailyMission{
    public Mission[] missions;
    public DateTime lastLogInTime;
    public int[] dailyCalcGameTotal;
    public int[] dailyNumGameTotal;
    public int[] dailyCalcRecordBreak;
    public int[] dailyCalcAnswerStrike;
    public int[] dailyMonsterKill;



    public DailyMission() {
        lastLogInTime = DateTime.Now;
        dailyCalcGameTotal = new int[12];
        dailyNumGameTotal = new int[12];
        dailyCalcRecordBreak = new int[12];
        dailyCalcAnswerStrike = new int[12];
        dailyMonsterKill = new int[12];
        CreateMission();
    }

    private void CreateMission() {
        missions = new Mission[4];
        //0-Daily Reward
        Mission m0 = new Mission(10, 0);
        missions[0] = m0;
        //1-10 Calculation Game
        Mission m1 = new Mission(30, 1);
        missions[1] = m1;
        //2-6 answer strike
        Mission m2 = new Mission(50, 2);
        missions[2] = m2;
        //3-7 record
        Mission m3 = new Mission(40, 3);
        missions[3] = m3;
    }
}



