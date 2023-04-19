using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class AI : MonoBehaviour {
    
    public Vector3 spawnPosition;
    public Quaternion spawnRotation;
    public Animator anim;
    public float hp = 100f;
    public Pamięć pamieć;
    Vector3 początkowaPozycja;
    public Pistolet pistolet;
    
    private int _wybranyCel;
    private Vector3 _wybranyCelVector3;
    private bool _śledzi;
    private int _aggro;
    private bool semafor;

    private NavMeshAgent _agent;

    public int Mode;
    public Transform Punkt1;
    public Transform Punkt2;
    public Transform Player;

    public Gracz gracz;
    public int damage = 30;
    
    private float WystarczajacaBlisko = 2f;
    private int delay = 15;

    public float Prendkosc = 0.1f;

    public float Celnosc;

    public float PościgPręndkość = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        ZmianaCelu(Punkt1.position, 1);
        _agent.speed = Prendkosc;
        lastPosition = transform.position;
        początkowaPozycja = transform.position;
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;
    }
    
    public void Resetuj () {
        transform.position = spawnPosition;
        transform.rotation = spawnRotation;
        ZmianaCelu(Punkt1.position, 1);
        _agent.speed = Prendkosc;
        lastPosition = transform.position;
        _śledzi = false;
    }
    
    float speed;
    Vector3 lastPosition;
    
    private void FixedUpdate()
    {
        if (Spot())
        {
            if (!_śledzi)
            {
                _śledzi = true;
                anim.SetBool("celuje", true);
                ZmianaCelu(Player.position, 3);
            }
            else
            {
                delay--;
                if (delay < 0)
                {
                    delay = Random.Range (10, 15);
                    pistolet.Wystrzał ();
                    if (Celnosc > Random.Range(1, 100))
                    {
                        gracz.Hp -= damage;
                        gracz.interfejs.PokażHp (gracz.Hp);
                    }
                }
            }
        }
        else if (_śledzi)
        {
            ZmianaCelu(Player.position, 3);       
        }
        else
        {
            var odleglosc = Vector3.Distance(transform.position, _wybranyCelVector3);
            if (odleglosc < WystarczajacaBlisko)
            {
                StartCoroutine(Action());
            }
        }
        
        speed = Mathf.Lerp(speed, (transform.position - lastPosition).magnitude, 0.7f /*adjust this number in order to make interpolation quicker or slower*/);
        lastPosition = transform.position;
        
        anim.SetFloat ("szybkość", speed);
        
        if (hp <= 0) {
            pamieć.zapamiętany = true;
            gameObject.SetActive (false);
        }
    }
    
    IEnumerator Action()
    {
        if (semafor)
        {
            
        }
        else
        {
            
            semafor = true;
            if (Mode == 1)
            {
                yield return new WaitForSeconds(25f);
                ZmianaCelu(Punkt2.position, 2);
            }
            else if (Mode == 2)
            {
                yield return new WaitForSeconds(10f);
                if (_wybranyCel == 1)
                {
                    ZmianaCelu(Punkt2.position, 2);
                }
                else if(_wybranyCel == 2)
                {
                    ZmianaCelu(Punkt1.position, 1);
                
                }
            }
            semafor = false;
        }
    }
    
    void ZmianaCelu(Vector3 nowyCel, int nowyIndeksCelu)
    {
        _wybranyCel = nowyIndeksCelu;
        _wybranyCelVector3 = nowyCel;
        _agent.destination = _wybranyCelVector3;
    }

    bool Spot()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, (Player.position + Vector3.up) - transform.position, out hit, 15))
        {
            if (hit.collider != null)
            {
                //Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.tag == "Player")
                {
                    var angle = Vector3.Angle(transform.forward, (Player.position + Vector3.up) - transform.position);
                    if (angle > -90 && angle < 90)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
