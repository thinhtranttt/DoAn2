using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerData {
    public int id;
    public User user;
    public Player player;

    [Serializable]
    public class User
    {
        public string userName;
        public string passWord;
        public User(string user, string pass)
        {
            this.userName = user;
            this.passWord = pass;
        }
        public User()
        {
            this.userName = "";
            this.passWord = "";
        }
    }    
    [Serializable]
    public class Player
    {
        public int id;
        public string name;
        public int avatarID;
        public int level;
        public double experience;
        public Digimon digimon;
        [Serializable]
        public class Digimon
        {
            public int id;
            public string name;
            public int avatarID;
            public int level;
            public double experience;
            public Digimon(int id, string name, int level,double exp)
            {
                this.id = id;
                this.name = name;
                this.avatarID = id;
                this.level = level;
                this.experience = exp;
            }
            public Digimon()
            {
                this.id = 0;
                this.name = "";
                this.avatarID = 0;
                this.level = 1;
                this.experience = 0;
            }
        }
        public Player(int id,string name,int level,double exp,Digimon dm)
        {
            this.id = id;
            this.name = name;
            this.avatarID = id;
            this.level = level;
            this.experience = exp;
            this.digimon = dm;
        }
        public Player(Digimon dm)
        {
            this.id = 0;
            this.name = "";
            this.avatarID = 0;
            this.level = 1;
            this.experience = 0;
            this.digimon = dm;
        }
        public Player()
        {
            this.id = 0;
            this.name = "";
            this.avatarID = 0;
            this.level = 1;
            this.experience = 0;
            this.digimon = null;
        }
    }    
    public PlayerData(int id,User u,Player pl)
    {
        this.id = id;
        this.user = u;
        this.player = pl;
    }
    public PlayerData()
    {

    }
}
