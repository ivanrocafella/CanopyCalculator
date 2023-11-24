using Assets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                Gost = profilePipeData.Gost
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
                Gost = e.Gost
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
        public static List<ProfilePipe> GetListProfilePipes(ProfilePipeDataList profilePipeDataList) =>
            profilePipeDataList.profilePipesData.Select( e =>
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
                Gost = e.Gost
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
    }
}
