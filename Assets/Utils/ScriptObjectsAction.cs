using Assets.Models;
using Assets.Scripts.SOdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine.Profiling;

namespace Assets.Utils
{
    public static class ScriptObjectsAction
    {
        public static ProfilePipe GetProfilePipe(ProfilePipeData profilePipeData) =>
            new()
            {
                Name = profilePipeData.Name,
                Height = profilePipeData.Height,
                Length = profilePipeData.Length,
                Thickness = profilePipeData.Thickness,
                Radius = profilePipeData.Radius,
                Area = profilePipeData.Area,
                MomentInertia = profilePipeData.MomentInertia,
                MomentResistance = profilePipeData.MomentResistance,
                WeightMeter = profilePipeData.WeightMeter,
                Gost = profilePipeData.Gost,
                PricePerM = profilePipeData.pricePerM
            };
        public static ProfilePipe GetProfilePipeByName(string name, ProfilePipeDataList profilePipeDataList) =>
            profilePipeDataList.profilePipesData.Select(e =>
            new ProfilePipe()
            {
                Name = e.Name,
                Height = e.Height,
                Length = e.Length,
                Thickness = e.Thickness,
                Radius = e.Radius,
                Area = e.Area,
                MomentInertia = e.MomentInertia,
                MomentResistance = e.MomentResistance,
                WeightMeter = e.WeightMeter,
                Gost = e.Gost,
                PricePerM = e.pricePerM
            }
            ).ToList().Find(e => e.Name == name);
        public static Truss GetTruss(TrussData trussData) =>
            new()
            {
                Name = trussData.Name,
                Height = trussData.Height,
                Gap = trussData.Gap,
                GapExter = trussData.GapExter,
                LengthCrate = trussData.LengthCrate,
                AngleCrateInDegree = trussData.AngleCrateInDegree,
                PricePerM = trussData.pricePerM,
                ProfileBelt =
                {
                    Name = trussData.profileBeltData.Name,
                    Height = trussData.profileBeltData.Height,
                    Length = trussData.profileBeltData.Length,
                    Thickness = trussData.profileBeltData.Thickness,
                    Radius = trussData.profileBeltData.Radius,
                    Area = trussData.profileBeltData.Area,
                    MomentInertia = trussData.profileBeltData.MomentInertia,
                    MomentResistance = trussData.profileBeltData.MomentResistance,
                    WeightMeter = trussData.profileBeltData.WeightMeter,
                    Gost = trussData.profileBeltData.Gost
                },
                ProfileCrate =
                {
                    Name = trussData.profileCrateData.Name,
                    Height = trussData.profileCrateData.Height,
                    Length = trussData.profileCrateData.Length,
                    Thickness = trussData.profileCrateData.Thickness,
                    Radius = trussData.profileCrateData.Radius,
                    Area = trussData.profileCrateData.Area,
                    MomentInertia = trussData.profileCrateData.MomentInertia,
                    MomentResistance = trussData.profileCrateData.MomentResistance,
                    WeightMeter = trussData.profileCrateData.WeightMeter,
                    Gost = trussData.profileCrateData.Gost
                }
            };
        public static Truss GetTrussByName(string name, TrussDataList trussDataList) =>
            trussDataList.trussesData.Select(e =>
            new Truss()
            {
                Name = e.Name,
                Height = e.Height,
                Gap = e.Gap,
                GapExter = e.GapExter,
                LengthCrate = e.LengthCrate,
                AngleCrateInDegree = e.AngleCrateInDegree,
                PricePerM = e.pricePerM,
                ProfileBelt =
                {
                    Name = e.profileBeltData.Name,
                    Height = e.profileBeltData.Height,
                    Length = e.profileBeltData.Length,
                    Thickness = e.profileBeltData.Thickness,
                    Radius = e.profileBeltData.Radius,
                    Area = e.profileBeltData.Area,
                    MomentInertia = e.profileBeltData.MomentInertia,
                    MomentResistance = e.profileBeltData.MomentResistance,
                    WeightMeter = e.profileBeltData.WeightMeter,
                    Gost = e.profileBeltData.Gost
                },
                ProfileCrate =
                {
                    Name = e.profileCrateData.Name,
                    Height = e.profileCrateData.Height,
                    Length = e.profileCrateData.Length,
                    Thickness = e.profileCrateData.Thickness,
                    Radius = e.profileCrateData.Radius,
                    Area = e.profileCrateData.Area,
                    MomentInertia = e.profileCrateData.MomentInertia,
                    MomentResistance = e.profileCrateData.MomentResistance,
                    WeightMeter = e.profileCrateData.WeightMeter,
                    Gost = e.profileCrateData.Gost
                }
            }
            ).ToList().Find(e => e.Name == name);
        public static Material GetMaterial(MaterialData materialData) =>
            new()
            {
                Name = materialData.Name,
                YieldStrength = materialData.YieldStrength,
                TensileStrength = materialData.TensileStrength,
                ElastiModulus = materialData.ElastiModulus
            };
        public static Material GetMaterialByName(string name, MaterialDataList materialDataList) =>
            materialDataList.materialsData.Select(e =>
            new Material()
            {
                Name = e.Name,
                YieldStrength = e.YieldStrength,
                TensileStrength = e.TensileStrength,
                ElastiModulus = e.ElastiModulus
            }
            ).ToList().Find(e => e.Name == name);

        public static DollarRate GetDollarRate(DollarRateData dollarRatelData) =>
            new()
            {
                Rate = dollarRatelData.rate
            };

        public static List<ProfilePipe> GetListProfilePipes(ProfilePipeDataList profilePipeDataList) =>
            profilePipeDataList.profilePipesData.Select(e =>
            new ProfilePipe()
            {
                Name = e.Name,
                Height = e.Height,
                Length = e.Length,
                Thickness = e.Thickness,
                Radius = e.Radius,
                Area = e.Area,
                MomentInertia = e.MomentInertia,
                MomentResistance = e.MomentResistance,
                WeightMeter = e.WeightMeter,
                Gost = e.Gost,
                PricePerM = e.pricePerM
            }
            ).ToList();
        public static List<Truss> GetListTrusses(TrussDataList trussDataList) =>
            trussDataList.trussesData.Select(e =>
            new Truss()
            {
                Name = e.Name,
                Height = e.Height,
                Gap = e.Gap,
                GapExter = e.GapExter,
                LengthCrate = e.LengthCrate,
                AngleCrateInDegree = e.AngleCrateInDegree,
                PricePerM = e.pricePerM,
                ProfileBelt =
                {
                    Name = e.profileBeltData.Name,
                    Height = e.profileBeltData.Height,
                    Length = e.profileBeltData.Length,
                    Thickness = e.profileBeltData.Thickness,
                    Radius = e.profileBeltData.Radius,
                    Area = e.profileBeltData.Area,
                    MomentInertia = e.profileBeltData.MomentInertia,
                    MomentResistance = e.profileBeltData.MomentResistance,
                    WeightMeter = e.profileBeltData.WeightMeter,
                    Gost = e.profileBeltData.Gost
                },
                ProfileCrate =
                {
                    Name = e.profileCrateData.Name,
                    Height = e.profileCrateData.Height,
                    Length = e.profileCrateData.Length,
                    Thickness = e.profileCrateData.Thickness,
                    Radius = e.profileCrateData.Radius,
                    Area = e.profileCrateData.Area,
                    MomentInertia = e.profileCrateData.MomentInertia,
                    MomentResistance = e.profileCrateData.MomentResistance,
                    WeightMeter = e.profileCrateData.WeightMeter,
                    Gost = e.profileCrateData.Gost
                }
            }
            ).ToList();

        public static List<Material> GetListMaterials(MaterialDataList materialDataList) =>
            materialDataList.materialsData.Select(e =>
            new Material()
            {
                Name = e.Name,
                YieldStrength = e.YieldStrength,
                TensileStrength = e.TensileStrength,
                ElastiModulus = e.ElastiModulus
            }
            ).ToList();

        public static List<MountUnitBeamRafterTruss> GetListMountUnitBeamRafterTrusses(MountUnitBeamRafterTrussDataList mountUnitBeamRafterTrussDataList) =>
            mountUnitBeamRafterTrussDataList.mountUnitBeamRafterTrussDatas.Select(e =>
            new MountUnitBeamRafterTruss()
            {
                MountUnitName = e.MountUnitName,
                RafterTrussName = e.RafterTrussName,
                SizeProfileRafterTruss = e.SizeProfileRafterTruss,
                WidthFlangeBeamTruss = e.WidthFlangeBeamTruss,
                LengthFlangeBeamTruss = e.LengthFlangeBeamTruss,
                ThicknessFlangeBeamTruss = e.ThicknessFlangeBeamTruss,
                ThicknessHeadScrew = e.ThicknessHeadScrew,
                ThicknessHeadNut = e.ThicknessHeadNut,
                ThicknessWasher = e.ThicknessWasher,
                WidthFlangeRafterTruss = e.WidthFlangeRafterTruss,
                LengthFlangeRafterTruss = e.LengthFlangeRafterTruss,
                ThicknessFlangeRafterTruss = e.ThicknessFlangeRafterTruss,
                CenterCenterDistance = e.CenterCenterDistance,
                NameFlangeBeam = e.NameFlangeBeam,
                NameFlangeRafter = e.NameFlangeRafter,
                NameScrew = e.NameScrew,
                NameNut = e.NameNut,
                NameWasher = e.NameWasher,
                WeightUnitScrew = e.WeightUnitScrew,
                WeightUnitNut = e.WeightUnitNut,
                WeightUnitWasher = e.WeightUnitWasher
            }
            ).ToList();

        public static MountUnitBeamRafterTruss GetMountUnitBeamRafterTrussByName(string name, MountUnitBeamRafterTrussDataList mountUnitBeamRafterTrussDataList) =>
            mountUnitBeamRafterTrussDataList.mountUnitBeamRafterTrussDatas.Select(e =>
            new MountUnitBeamRafterTruss()
            {
                MountUnitName = e.MountUnitName,
                RafterTrussName = e.RafterTrussName,
                SizeProfileRafterTruss = e.SizeProfileRafterTruss,
                WidthFlangeBeamTruss = e.WidthFlangeBeamTruss,
                LengthFlangeBeamTruss = e.LengthFlangeBeamTruss,
                ThicknessFlangeBeamTruss = e.ThicknessFlangeBeamTruss,
                ThicknessHeadScrew = e.ThicknessHeadScrew,
                ThicknessHeadNut = e.ThicknessHeadNut,
                ThicknessWasher = e.ThicknessWasher,
                WidthFlangeRafterTruss = e.WidthFlangeRafterTruss,
                LengthFlangeRafterTruss = e.LengthFlangeRafterTruss,
                ThicknessFlangeRafterTruss = e.ThicknessFlangeRafterTruss,
                CenterCenterDistance = e.CenterCenterDistance,
                NameFlangeBeam = e.NameFlangeBeam,
                NameFlangeRafter = e.NameFlangeRafter,
                NameScrew = e.NameScrew,
                NameNut = e.NameNut,
                NameWasher = e.NameWasher,
                WeightUnitScrew = e.WeightUnitScrew,
                WeightUnitNut = e.WeightUnitNut,
                WeightUnitWasher = e.WeightUnitWasher
            }
            ).ToList().Find(e => e.RafterTrussName == name);

        public static List<MountUnitColumnBeamTruss> GetListMountUnitColumnBeamTrusses(MountUnitColumnBeamTrussDataList mountUnitColumnBeamTrussDataList) =>
           mountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas.Select(e =>
           new MountUnitColumnBeamTruss()
           {
               MountUnitName = e.MountUnitName,
               BeamTrussName = e.BeamTrussName,
               SizeProfileBeamTruss = e.SizeProfileBeamTruss,
               WidthFlangeColumn = e.WidthFlangeColumn,
               LengthFlangeColumn = e.LengthFlangeColumn,
               ThicknessFlangeColumn = e.ThicknessFlangeColumn,
               WidthFlangeBeamTruss = e.WidthFlangeBeamTruss,
               LengthFlangeBeamTruss = e.LengthFlangeBeamTruss,
               ThicknessFlangeBeamTruss = e.ThicknessFlangeBeamTruss,
               ThicknessHeadScrew = e.ThicknessHeadScrew,
               ThicknessHeadNut = e.ThicknessHeadNut,
               ThicknessWasher = e.ThicknessWasher,
               CenterCenterDistance = e.CenterCenterDistance,
               NameFlangeColumn = e.NameFlangeColumn,
               NameFlangeBeam = e.NameFlangeBeam,
               NameScrew = e.NameScrew,
               NameNut = e.NameNut,
               NameWasher = e.NameWasher,
               WeightUnitScrew = e.WeightUnitScrew,
               WeightUnitNut = e.WeightUnitNut,
               WeightUnitWasher = e.WeightUnitWasher
           }
           ).ToList();

        public static MountUnitColumnBeamTruss GetMountUnitColumnBeamTrussByName(string name, MountUnitColumnBeamTrussDataList mountUnitColumnBeamTrussDataList) =>
            mountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas.Select(e =>
            new MountUnitColumnBeamTruss()
            {
                MountUnitName = e.MountUnitName,
                BeamTrussName = e.BeamTrussName,
                SizeProfileBeamTruss = e.SizeProfileBeamTruss,
                WidthFlangeColumn = e.WidthFlangeColumn,
                LengthFlangeColumn = e.LengthFlangeColumn,
                ThicknessFlangeColumn = e.ThicknessFlangeColumn,
                WidthFlangeBeamTruss = e.WidthFlangeBeamTruss,
                LengthFlangeBeamTruss = e.LengthFlangeBeamTruss,
                ThicknessFlangeBeamTruss = e.ThicknessFlangeBeamTruss,
                ThicknessHeadScrew = e.ThicknessHeadScrew,
                ThicknessHeadNut = e.ThicknessHeadNut,
                ThicknessWasher = e.ThicknessWasher,
                CenterCenterDistance = e.CenterCenterDistance,
                NameFlangeColumn = e.NameFlangeColumn,
                NameFlangeBeam = e.NameFlangeBeam,
                NameScrew = e.NameScrew,
                NameNut = e.NameNut,
                NameWasher = e.NameWasher,
                WeightUnitScrew = e.WeightUnitScrew,
                WeightUnitNut = e.WeightUnitNut,
                WeightUnitWasher = e.WeightUnitWasher
            }
            ).ToList().Find(e => e.BeamTrussName == name);

        public static List<Flange> GetListFlange(MountUnitColumnBeamTrussDataList mountUnitColumnBeamTrussDataList
            , MountUnitBeamRafterTrussDataList mountUnitBeamRafterTrussDataList)
        {
            IEnumerable<Flange> GetFlanges<T>(IEnumerable<T> source
                , Func<T, string> selectorName
                , Func<T, int> selectorWidth
                , Func<T, int> selectorLength
                , Func<T, float> selectorCenterCenterDistance) => source.Select(e => new Flange { Name = selectorName(e)
                                                                                                , Width = selectorWidth(e)
                                                                                                , Length = selectorLength(e)
                                                                                               , CenterCenterDistance = selectorCenterCenterDistance(e) });

            List<Flange> flanges = GetFlanges(mountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas
                , e => e.NameFlangeColumn, e => e.WidthFlangeColumn, e => e.LengthFlangeColumn, e => e.CenterCenterDistance).ToList();
            flanges.AddRange(GetFlanges(mountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas
                , e => e.NameFlangeBeam, e => e.WidthFlangeBeamTruss, e => e.LengthFlangeBeamTruss, e => e.CenterCenterDistance));
            flanges.AddRange(GetFlanges(mountUnitBeamRafterTrussDataList.mountUnitBeamRafterTrussDatas
                  , e => e.NameFlangeBeam, e => e.WidthFlangeBeamTruss, e => e.LengthFlangeBeamTruss, e => e.CenterCenterDistance));
            flanges.AddRange(GetFlanges(mountUnitBeamRafterTrussDataList.mountUnitBeamRafterTrussDatas
                  , e => e.NameFlangeRafter, e => e.WidthFlangeRafterTruss, e => e.LengthFlangeRafterTruss, e => e.CenterCenterDistance));
            return flanges.Distinct(new GenericEqualityComparer<Flange>(e => e.Name)).ToList();
        }

        public static List<Fixing> GetListFixing(MountUnitColumnBeamTrussDataList mountUnitColumnBeamTrussDataList
            , MountUnitBeamRafterTrussDataList mountUnitBeamRafterTrussDataList)
        {
            IEnumerable<Fixing> GetFixings<T>(IEnumerable<T> source, Func<T, string> selector) => source.Select(e => new Fixing { Name = selector(e) });

            List<Fixing> fixings = GetFixings(mountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas, e => e.NameScrew).ToList();
            fixings.AddRange(GetFixings(mountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas, e => e.NameNut).ToList());
            fixings.AddRange(GetFixings(mountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas, e => e.NameWasher).ToList());
            fixings.AddRange(GetFixings(mountUnitBeamRafterTrussDataList.mountUnitBeamRafterTrussDatas, e => e.NameScrew).ToList());
            fixings.AddRange(GetFixings(mountUnitBeamRafterTrussDataList.mountUnitBeamRafterTrussDatas, e => e.NameNut).ToList());
            fixings.AddRange(GetFixings(mountUnitBeamRafterTrussDataList.mountUnitBeamRafterTrussDatas, e => e.NameWasher).ToList());
            return fixings.Distinct(new GenericEqualityComparer<Fixing>(e => e.Name)).ToList();
        }
    }
}
