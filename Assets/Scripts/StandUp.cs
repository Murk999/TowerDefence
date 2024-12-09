using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class StandUp : MonoBehaviour
    {
        private Rigidbody2D rig;
        private SpriteRenderer sr;
        
        private void Start()
        {
            rig = transform.root.GetComponent<Rigidbody2D>(); // получаем трансформу риджидбади
            sr = GetComponent<SpriteRenderer>(); // получаем спрайт обьекта

        }
        

        private void LateUpdate() // позднее обновление 
        {
            transform.up = Vector2.up; // всегда смотрим вверх
            
            var xMotion = rig.velocity.x;
            if (xMotion > 0.01f) sr.flipX = false;
            else if(xMotion < 0.01f) sr.flipX=true;
        }
    }
}
