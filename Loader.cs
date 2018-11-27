using UnityEngine;
using System.Collections;

namespace seekofknowledge
{
    public class Loader : MonoBehaviour
    {
        public GameObject gameManager;          //GameManager prefab to instantiate.
        public GameObject soundManager;         //SoundManager prefab to instantiate.


        void Awake()
        {
            //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
            if (Game.instance == null)

                //Instantiate gameManager prefab
                Instantiate(gameManager);
            if (SoundManager.instance == null)

                //Instantiate SoundManager prefab
                Instantiate(soundManager);
        }
    }
}