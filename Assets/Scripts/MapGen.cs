using System;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class MapGen : MonoBehaviour
{
    [Header("Floor Events")]
    [SerializeField]
    private Spawner spawner = null;
    private Random rnd;

    [Header("Generation Settings")]
    public bool generateMarkers = false;
    public bool generateGlimpses = true;
    public int floorAmount;
    public int seed;

    #region mapFiles
    [Header("Map Files")]
    public GameObject FLOOR_PREFAB;
    public GameObject floor_default;
    public GameObject floor_start;
    public GameObject floor_window;
    public GameObject floor_smallhall;
    public GameObject floor_bighall;
    public GameObject floor_hole;
    public GameObject floor_holealt;
    public GameObject floor_choice;
    public GameObject floor_maze;
    public GameObject floor_new1;
    public GameObject floor_drop;
    public GameObject floor_drop_path;
    public GameObject fMarker;
    #endregion

    [Header("Audio")]
    public AudioClip loudStep;
    public AudioClip loudBreath;
    public AudioClip[] radioClips = new AudioClip[5];

    private void Start()
    {
        Globals.M_GEN = this;
        CreateMap();
        Enemy.CreateEnemy(new Vector3(1, -3f, 1), false);
        if (generateGlimpses) spawner.CreateGlimpses();
    }

    public void CreateMap()
    {
        if (floorAmount == 0)
        {
            throw new UnityException("Map cannot have 0 floors.");
        }

        Debug.Log(string.Format("Creating map with {0} floors, seed {1}.", floorAmount, seed));
        Floor.floors = new Floor[floorAmount];

        rnd = new Random(seed);
        Floor.mapGenRandom = rnd;

        Floor.CreateFloor<FloorProceed>(0);

        Floor.CreateFloor<Floor>(1);

        Floor.CreateFloor<FloorRadio2>(rnd.Next(3, 5));

        Floor.CreateFloor<FloorRadio3>(rnd.Next(5, 7));

        Floor.CreateFloor<FloorLock>(7);

        Floor.CreateFloor<FloorRadio4>(rnd.Next(8, 10));

        Floor.CreateFloor<FloorSound>(rnd.Next(10, 12), 1);

        Floor.CreateFloor<FloorSound>(rnd.Next(12, 14));

        Floor.CreateFloor<FloorFlash>(rnd.Next(10, 20));

        Floor.CreateFloor<FloorLights>(rnd.Next(20, 23));

        switch (rnd.Next(1, 5))
        {
            case 1:
                Floor.CreateFloor<FloorTrick1>(rnd.Next(25, 29));
                break;
            case 2:
                Floor.CreateFloor<FloorTrick2>(rnd.Next(25, 29));
                break;
        }

        Floor.CreateFloor<FloorRun>(rnd.Next(29, 34));

        Floor.CreateFloor<Floor173>(rnd.Next(34, 38));

        Floor.CreateFloor<FloorRun>(rnd.Next(40, 61));

        Floor.CreateFloor<FloorRoar>(rnd.Next(40, 61));

        Floor.CreateFloor<FloorTrap>(rnd.Next(40, 61));

        Floor.CreateFloor<FloorFlash>(rnd.Next(40, 61));

        int pos = 0;
        bool free = false;
        for (int i = 0; i < 8; i++)
        {
            do
            {
                pos = rnd.Next(25, 70);
                if (Floor.floors[pos] == null)
                {
                    free = true;
                }
            } while (!free);

            switch (rnd.Next(1, 11))
            {
                case 1:
                case 9:
                    Floor.CreateFloor<FloorCell>(pos);
                    break;
                case 2:
                    Floor.CreateFloor<FloorFlash>(pos);
                    break;
                case 3:
                    Floor.CreateFloor<FloorTrick1>(pos);
                    break;
                case 4:
                    Floor.CreateFloor<FloorTrick2>(pos);
                    break;
                case 5:
                    Floor.CreateFloor<FloorSound>(pos, 1);
                    break;
                case 6:
                    Floor.CreateFloor<FloorSound>(pos);
                    break;
                case 7:
                    Floor.CreateFloor<FloorTrap>(pos);
                    break;
                case 8:
                    Floor.CreateFloor<FloorRoar>(pos);
                    break;
            }
        }

        for (int j = 0; j < 60; j++)
        {
            do
            {
                pos = rnd.Next(75, 201);
                if (Floor.floors[pos] == null)
                {
                    free = true;
                }
            } while (!free);

            switch (rnd.Next(1, 11))
            {
                case 1: //FALLTHROUGH
                case 9:
                    Floor.CreateFloor<FloorCell>(pos);
                    break;
                case 2:
                    Floor.CreateFloor<FloorFlash>(pos);
                    break;
                case 3:
                    Floor.CreateFloor<FloorTrick1>(pos);
                    break;
                case 4:
                    Floor.CreateFloor<FloorTrick2>(pos);
                    break;
                case 5:
                    Floor.CreateFloor<FloorSound>(pos, 1);
                    break;
                case 6:
                    Floor.CreateFloor<FloorSound>(pos);
                    break;
                case 7:
                    Floor.CreateFloor<FloorTrap>(pos);
                    break;
                case 8:
                    Floor.CreateFloor<FloorRoar>(pos);
                    break;
            }
        }

        Floor.CreateFloor<FloorDarkness>(rnd.Next(150, 201));

        GameObject tmp;

        for (int k = 0; k < floorAmount; k++)
        {
            if (k == 0)
            {
                tmp = Instantiate(floor_start) as GameObject;
            }
            else
            {
                if (Floor.floors[k] == null)
                {
                    Floor.CreateFloor<Floor>(k);
                    switch (rnd.Next(1, 24))
                    {
                        case 1:
                        case 2:
                            tmp = Instantiate(floor_window);
                            break;
                        case 3:
                        case 4:
                            tmp = Instantiate(floor_smallhall);
                            break;
                        case 5:
                        case 6:
                            tmp = Instantiate(floor_bighall);
                            break;
                        case 7:
                            tmp = Instantiate(floor_hole);
                            break;
                        case 8:
                            tmp = Instantiate(floor_holealt);
                            break;
                        case 9:
                            tmp = Instantiate(floor_choice);
                            break;
                        case 10:
                            if (k > 40)
                            {
                                tmp = Instantiate(floor_maze);
                            }
                            else
                            {
                                tmp = Instantiate(floor_default);
                            }
                            break;
                        case 11:
                            tmp = Instantiate(floor_new1);
                            break;
                        case 12:
                            if (k > 50)
                            {
                                tmp = Instantiate(floor_drop_path);
                            }
                            else
                            {
                                tmp = Instantiate(floor_drop);
                            }
                            break;
                        default:
                            tmp = Instantiate(floor_default);
                            break;
                    }
                }
                else
                {
                    switch (Floor.floors[k].ID)
                    {
                        case Floor.ACT_173:
                            tmp = Instantiate(floor_smallhall) as GameObject;
                            break;
                        case Floor.ACT_CELL:
                            tmp = Instantiate(floor_window) as GameObject;
                            break;
                        case Floor.ACT_TRICK1:
                            tmp = Instantiate(floor_hole) as GameObject;
                            break;
                        case Floor.ACT_TRICK2:
                            tmp = Instantiate(floor_holealt) as GameObject;
                            break;
                        default:
                            tmp = Instantiate(floor_default) as GameObject;
                            break;
                    }
                }
            }

            tmp.transform.parent = Floor.floors[k].transform;
            Floor.floors[k].actualFloor = tmp;
            Floor.floors[k].mRend = tmp.GetComponentInChildren<MeshRenderer>();
            if (k % 2 == 0)
            {
                Floor.floors[k].transform.position = new Vector3(0, (-k * 3.0f), 0);
            }
            else
            {
                Floor.floors[k].transform.RotateAround(tmp.GetComponentInChildren<MeshRenderer>().bounds.center, Vector3.up, 180);
                Floor.floors[k].transform.position = new Vector3(-12, (-k * 3.0f), 10.5f);
            }
            Floor.floors[k].mRend.enabled = false;
        }
        Floor.floors[0].mRend.enabled = true;
        Floor.floors[1].mRend.enabled = true;

        for (int i = 0; i < Floor.floors.Length; i++)
        {
            Floor.floors[i].transform.SetSiblingIndex(i);
        }

        Debug.Log("Generated map succesfully.");
        /*DController.activity.Details = "Inside SCP-087-B.";
        DController.activity.Timestamps.Start = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
        DController.UpdateFloors();*/

        if (generateMarkers == true) CreateMarkers();
        else return;
    }

    public void CreateMarkers()
    {
        Debug.Log(string.Format("Generating {0} floor markers...", floorAmount));
        string number;
        for (int q = 1; q < floorAmount; q++)
        {
            switch (rnd.Next(1, 701))
            {
                case 1:
                    number = string.Empty;
                    break;
                case 2:
                    number = rnd.Next(1200, 2401).ToString();
                    break;
                case 3:
                    number = "NIL";
                    break;
                case 4:
                    number = "?";
                    break;
                case 5:
                    number = "NO";
                    break;
                case 6:
                    number = "stop";
                    break;
                case 7:
                    number = "CA T ON";
                    break;
                case 8:
                    number = "pro l m";
                    break;
                case 9:
                    number = "d e";
                    break;
                default:
                    number = q.ToString();
                    break;
            }

            if (q > 100 && rnd.NextDouble() < Mathf.Min(0.95f, 0.00015f * Mathf.Pow(q - 100, 2)))
            {
                number = string.Empty;
                for (int n = 1; n < rnd.Next(1, 5); n++)
                {
                    number = (number + (char)(rnd.Next(33, 123 + 1) % 255));
                }
            }

            GameObject fmark = Instantiate(fMarker) as GameObject;
            fmark.transform.parent = Floor.floors[q].transform;
            fmark.transform.position = new Vector3(0.7f, 0.3f, -0.51f);

            fmark.transform.Find("FloorMarkerText").GetComponent<TMP_Text>().text = number;

            if (q % 2 == 0)
            {
                fmark.transform.position = new Vector3(0.43f, -q * 3 - 1f, 0.75f);
                fmark.transform.rotation *= Quaternion.Euler(0, 180, 0);
            }
            else
            {
                fmark.transform.position = new Vector3(-12.43f, -q * 3 - 1f, 9.75f);
            }
        }
        Debug.Log("Generated markers succesfully.");
    }

}
