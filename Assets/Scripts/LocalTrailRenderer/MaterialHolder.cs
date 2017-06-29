using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MaterialHolder : MonoBehaviour
{
    public static MaterialHolder MH;

    public List<Material> FundamentalsTrail = new List<Material>();
    public List<Material> EffectsTrail = new List<Material>();

    public enum Fundamentals { Air, Water, Earth, Fire, Light, Dark, Bard }
    public enum Effects { Bolt, AreaOfEffect, Beam, Cone, DelayedAreaOfEffect, Enchantment, ExplosiveBall, Mine, MinionSummon, Protection, WeaponSummon }

    private void Start()
    {
        MH = this;
    }

    public static Material GetFundamentalMaterial(Fundamentals Fundament)
    {
        return MH.FundamentalsTrail[(int)Fundament];
    }

    public static Material GetEffectMaterial(Effects Effect)
    {
        return MH.EffectsTrail[(int)Effect];
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(MaterialHolder))]
public class MaterialHolderEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Sort Fundamentals"))
        {
            SortFundamentals();
        }

        if(GUILayout.Button("Sort Effects"))
        {
            SortEffects();
        }

    }

    void SortFundamentals()
    {
        List<Material> SortedList = new List<Material>();
        MaterialHolder myTarget = (MaterialHolder)target;
        for(int i = 0; i < myTarget.FundamentalsTrail.Count; ++i)
        {
            SortedList.Add(myTarget.FundamentalsTrail.Find(x => x.name == ((MaterialHolder.Fundamentals)i).ToString()));
        }
        myTarget.FundamentalsTrail.Clear();
        myTarget.FundamentalsTrail.AddRange(SortedList);
    }

    void SortEffects()
    {
        List<Material> SortedList = new List<Material>();
        MaterialHolder myTarget = (MaterialHolder)target;
        for (int i = 0; i < myTarget.EffectsTrail.Count; ++i)
        {
            SortedList.Add(myTarget.EffectsTrail.Find(x => x.name == ((MaterialHolder.Effects)i).ToString()));
        }
        myTarget.EffectsTrail.Clear();
        myTarget.EffectsTrail.AddRange(SortedList);
    }

}
#endif