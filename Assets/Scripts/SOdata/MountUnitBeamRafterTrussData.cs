using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Assets.Scripts.SOdata
{
    [CreateAssetMenu(fileName = "New MountUnitBeamRafterTrussData", menuName = "MountUnitBeamRafterTruss Data", order = 60)]
    public class MountUnitBeamRafterTrussData : ScriptableObject
    {
        [SerializeField]
        private string mountUnitName;
        [SerializeField]
        private string rafterTrussName;
        [SerializeField]
        private int sizeProfileRafterTruss;
        [SerializeField]
        private int widthFlangeRafterTruss;
        [SerializeField]
        private int lengthFlangeRafterTruss;
        [SerializeField]
        private int thicknessFlangeRafterTruss;
        [SerializeField]
        private int widthFlangeBeamTruss;
        [SerializeField]
        private int lengthFlangeBeamTruss;
        [SerializeField]
        private int thicknessFlangeBeamTruss;
        [SerializeField]
        private int thicknessTable;
        [SerializeField]
        private float thicknessHeadScrew;
        [SerializeField]
        private float thicknessHeadNut;
        [SerializeField]
        private float thicknessWasher;      
        [SerializeField]
        private float centerCenterDistance;
        [SerializeField]
        private string nameFlangeBeam;
        [SerializeField]
        private string nameFlangeRafter;
        [SerializeField]
        private GameObject flangeBeamTruss;
        [SerializeField]
        private GameObject flangeRafterTruss;
        [SerializeField]
        private GameObject screw;
        [SerializeField]
        private string nameScrew;
        [SerializeField]
        private float weightUnitScrew;
        [SerializeField]
        private GameObject nut;
        [SerializeField]
        private string nameNut;
        [SerializeField]
        private float weightUnitNut;
        [SerializeField]
        private GameObject washer;
        [SerializeField]
        private string nameWasher;
        [SerializeField]
        private float weightUnitWasher;

        public string MountUnitName { get => mountUnitName; }
        public string RafterTrussName { get => rafterTrussName; }
        public int SizeProfileRafterTruss { get => sizeProfileRafterTruss; }
        public int WidthFlangeBeamTruss { get => widthFlangeBeamTruss; }
        public int LengthFlangeBeamTruss { get => lengthFlangeBeamTruss; }
        public int ThicknessFlangeBeamTruss { get => thicknessFlangeBeamTruss; }
        public int ThicknessTable { get => thicknessTable; }
        public float ThicknessHeadScrew { get => thicknessHeadScrew; }
        public float ThicknessHeadNut { get => thicknessHeadNut; }
        public float ThicknessWasher { get => thicknessWasher; }
        public int WidthFlangeRafterTruss { get => widthFlangeRafterTruss; }
        public int LengthFlangeRafterTruss { get => lengthFlangeRafterTruss; }
        public int ThicknessFlangeRafterTruss { get => thicknessFlangeRafterTruss; }
        public float CenterCenterDistance { get => centerCenterDistance; }
        public string NameFlangeBeam { get => nameFlangeBeam; }
        public string NameFlangeRafter { get => nameFlangeRafter; }
        public GameObject FlangeBeamTruss { get => flangeBeamTruss; }
        public GameObject FlangeRafterTruss { get => flangeRafterTruss; }
        public GameObject Screw { get => screw; }
        public string NameScrew { get => nameScrew; }
        public float WeightUnitScrew { get => weightUnitScrew; }
        public GameObject Nut { get => nut; }
        public string NameNut { get => nameNut; }
        public float WeightUnitNut { get => weightUnitNut; }
        public GameObject Washer { get => washer; }
        public string NameWasher { get => nameWasher; }
        public float WeightUnitWasher { get => weightUnitWasher; }
    }
    }
