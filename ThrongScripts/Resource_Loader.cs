using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource_Loader : MonoBehaviour
{
    //projectals
    private GameObject artilleryMissle;
    public GameObject ArtilleryMissle { get { return artilleryMissle; } set { artilleryMissle = value; } }

    //fx
    private GameObject artilleryMissle_fx;
    public GameObject ArtilleryMissle_fx { get { return artilleryMissle_fx; } set { artilleryMissle_fx = value; } }

    private GameObject bloodExplosion_fx;
    public GameObject BloodExplosion_fx { get { return bloodExplosion_fx; } set { bloodExplosion_fx = value; } }

    /// structures
    private GameObject sniperStructure;

    private GameObject turretStructure;

    private GameObject artileryStructure;

    private GameObject ironMinerStructure;

    private GameObject oilMinerStructure;

    private GameObject[] structuresHolder;
    public GameObject[] StructuresHolder { get { return structuresHolder; } set { } }

    // enemies
    private GameObject zealed_Guardian;
    public GameObject Zealed_Guardian { get { return zealed_Guardian; } set { zealed_Guardian = value; } }

    private GameObject zealed_Giant;
    public GameObject Zealed_Giant { get { return zealed_Giant; } set { zealed_Giant = value; } }

    private GameObject zealed_Boss;
    public GameObject Zealed_Boss { get { return zealed_Boss; } set { zealed_Boss = value; } }

    //load all the needed resources so they can be instanciated and being preloaded
    private void Awake()
    {
        bloodExplosion_fx = Resources.Load<GameObject>("FX/BloodFx/BloodFx");
        turretStructure = Resources.Load<GameObject>("Structures/TurretStructure");
        artileryStructure = Resources.Load<GameObject>("Structures/ArtilleryStructure");
        oilMinerStructure = Resources.Load<GameObject>("Structures/OilStructure");
        ironMinerStructure = Resources.Load<GameObject>("Structures/IronStructure");
        artilleryMissle = Resources.Load<GameObject>("Projectals/ArtileryProjectal");
        artilleryMissle_fx = Resources.Load<GameObject>("Projectals/ArtileryProjectalFx");
        sniperStructure = Resources.Load<GameObject>("Structures/SniperStructure");

        GameObject[] temp_structures = { ironMinerStructure, oilMinerStructure, turretStructure, artileryStructure, sniperStructure};
        structuresHolder = temp_structures;

        zealed_Guardian = Resources.Load<GameObject>("Enemies/Guardian_Ai");
        zealed_Giant = Resources.Load<GameObject>("Enemies/Giant_Ai");
        zealed_Boss = Resources.Load<GameObject>("Enemies/Boss_Ai");
    }
}
