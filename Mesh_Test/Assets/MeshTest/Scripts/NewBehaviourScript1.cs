using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefTestClass
{
    public int testVal = 0;
}

public class NewBehaviourScript1 : MonoBehaviour
{
    

    void Start()
    {
        int[] ex1 = { 1, 2, 3, 4, 5 };
        int[] ex2 = { 2, 4, 6, 8 };
        int[] newArray = new int[5];
        List<int> ss = new List<int>();
        ss.AddRange(ex1);

        Debug.Log("=========================111=======================");
        for (int i = 0; i < ss.Count; i++)
        {
            Debug.Log(ss[i]);
        }
        Debug.Log("=========================222=======================");

        ccc(ss);

        Debug.Log("=========================333=======================");
        for (int i = 0; i < ss.Count; i++)
        {
            Debug.Log(ss[i]);
        }





        //int index = 0;
        //foreach(int num in ex1)
        //{
        //    newArray[index++] = num;           
        //}
        //foreach(int num in newArray)
        //{
        //    Debug.Log("newArray num : " + num);
        //}
        //Debug.Log("newArray.Length : "+ newArray.Length);

        //int[] ex3 = { 1, 3, 5, 7, 9 };
        //int[] ex4 = { 8, 6, 4, 2 };
        //int[] newArray2 = new int[ex3.Length + ex4.Length];

        //ex3.CopyTo(newArray2, 0);
        //ex4.CopyTo(newArray2, ex3.Length);

        //foreach (int num in newArray2)
        //{
        //    Debug.Log("newArray2 num : " + num);
        //}

        //int[] bbb = aaa(ex1, ex2);

        //foreach (int num in bbb)
        //{
        //    Debug.Log("bbb num : " + num);
        //}
        //Array.Resize(ref ex1, 10);
        //foreach (int num in ex1)
        //{
        //    Debug.Log(" num : " + num);
        //}
    }
    //public int[] SetSizeArray(int[] aaa, int size, int plusNum)
    //{
    //    List<int> tmp = new List<int>();
    //    tmp.AddRange(aaa);
    //    tmp.Add(plusNum)

    //    for(int i = 0; i < size; i++)
    //    {
    //        if (aaa.Length > i)
    //        {
    //            tmp.Add(aaa[i]);
    //        }
    //        else
    //            tmp.Add(plusNum);
    //    }
    //    return tmp.ToArray();
    //}

    public void ccc(List<int> refv)
    {
        for(int i = 0; i < refv.Count; i++)
        {
            refv[i] += 1;
            Debug.Log(refv[i]);
        }
    }

    public void bbb(int[] tmp)
    {
        for(int i = 0; i < tmp.Length; i++)
        {
            tmp[i] += 1;
            Debug.Log(tmp[i]);
        }
    }

    public int[] aaa(int[] tmp, int[] plusnums)
    {
        int size = tmp.Length + plusnums.Length;
        int[] newTmp = new int[size];
        for(int i = 0; i<size; i++)
        {
            if (i > tmp.Length)
            {
                for(int j = 0; j<plusnums.Length; j++)
                {
                    newTmp[i + j] = plusnums[j];
                }
                break;
            }
            newTmp[i] = tmp[i];
            
        }
        return newTmp;
    }

    //public int[] aaa(int[] tmp, int[] plusnums)
    //{
    //    int size = tmp.Length + plusnums.Length;
    //    int[] newTmp = new int[size];

    //    for (int i = 0; i < tmp.Length; i++)
    //    {
    //        newTmp[i] = tmp[i];
    //    }

    //    for (int j = 0; j < plusnums.Length; j++)
    //    {
    //        newTmp[tmp.Length + j] = plusnums[j];
    //    }

    //    return newTmp;
    //}
}
