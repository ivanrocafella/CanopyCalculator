using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SOdata
{
    [CreateAssetMenu(fileName = "New MountUnitColumnBeamTrussData", menuName = "MountUnitColumnBeamTruss Data", order = 58)]
    public class MountUnitColumnBeamTrussData : ScriptableObject
    {
        [SerializeField]
        private string mountUnitName;
        [SerializeField]
        private string beamTrussName;
        [SerializeField]
        private int sizeProfileBeamTruss;
        [SerializeField]
        private int widthFlangeColumn;
        [SerializeField]
        private int lengthFlangeColumn;
        [SerializeField]
        private int thicknessFlangeColumn;
        [SerializeField]
        private float thicknessHeadScrew;
        [SerializeField]
        private float thicknessHeadNut;
        [SerializeField]
        private float thicknessWasher;
        [SerializeField]
        private int widthFlangeBeamTruss;
        [SerializeField]
        private int lengthFlangeBeamTruss;
        [SerializeField]
        private int thicknessFlangeBeamTruss;
        [SerializeField]
        private float centerCenterDistance;
        [SerializeField]
        private GameObject flangeBeamTruss;
        [SerializeField]
        private GameObject flangeColumn;
        [SerializeField]
        private GameObject screw;
        [SerializeField]
        private GameObject nut;
        [SerializeField]
        private GameObject washer;

        public string MountUnitName { get => mountUnitName; }
        public string BeamTrussName { get => beamTrussName; }
        public int SizeProfileBeamTruss { get => sizeProfileBeamTruss; }
        public int WidthFlangeColumn { get => widthFlangeColumn; }
        public int LengthFlangeColumn { get => lengthFlangeColumn; }
        public int ThicknessFlangeColumn { get => thicknessFlangeColumn; }
        public int WidthFlangeBeamTruss { get => widthFlangeBeamTruss; }
        public int LengthFlangeBeamTruss { get => lengthFlangeBeamTruss; }
        public int ThicknessFlangeBeamTruss { get => thicknessFlangeBeamTruss; }
        public float ThicknessHeadScrew { get => thicknessHeadScrew; }
        public float ThicknessHeadNut { get => thicknessHeadNut; }
        public float ThicknessWasher { get => thicknessWasher; }
        public float CenterCenterDistance { get => centerCenterDistance; }
        public GameObject FlangeBeamTruss { get => flangeBeamTruss; }
        public GameObject FlangeColumn { get => flangeColumn; }
        public GameObject Screw { get => screw; }
        public GameObject Nut { get => nut; }
        public GameObject Washer { get => washer; }
    }
    }
