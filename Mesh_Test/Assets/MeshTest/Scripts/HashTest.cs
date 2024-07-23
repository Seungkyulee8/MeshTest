using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class HashTest :MonoBehaviour
{
    /*
    Vector3 vertext;
    Vector2 uv;
    Vector3 normal;
    */
    
    private void Start()
    {
        StartCoroutine(Test());
    }

    IEnumerator Test()
    {
        HashSet<HashClass> hashClasses = new HashSet<HashClass>();

        HashClass obj1 = new HashClass(new Vector3(1.3f, 1.2f, 1.1f), new Vector2(1, 0), new Vector3(2, 2, 2));
        Debug.Log("add obj1");
        hashClasses.Add(obj1);
        Debug.Log("add end obj1");
        Debug.Log("------------------------------------------------------------");
        HashClass obj2 = new HashClass(new Vector3(3, 3, 3), new Vector2(0, 1), new Vector3(4, 4, 4));

        Debug.Log("add obj2");
        hashClasses.Add(obj2);
        Debug.Log("add end obj2");
        Debug.Log("------------------------------------------------------------");
        //HashClass obj3 = new HashClass(new Vector3(1, 1, 1), new Vector2(1, 0), new Vector3(2, 2, 2));

        //Debug.Log("add obj3");
        //hashClasses.Add(obj3);
        //Debug.Log("add end obj3");


        //Debug.Log("------------------------------------------------------------");
        //HashClass obj4 = new HashClass("123", "321");
        //hashClasses.Add(obj4);
        yield return null;
    }


}
public class HashClass
{
    public Vector3 vertex;
    public Vector2 uv;
    public Vector3 normal;
    private GameObject oobj = null;
    Vector3 defaultvector3 = new Vector3(0, 0, 0);
    Vector2 defaultvector2 = new Vector2(0, 0);

    int returnHashCode;

    public HashClass(Vector3 v, Vector2 vt, Vector3 vn)
    {
        vertex = v;
        uv = vt;
        normal = vn;


       // CalculateHashCode();
    }

    public HashClass(Vector3 v, Vector2 vt)
    {
        vertex = v;
        uv = vt;
        normal = defaultvector3;

       // CalculateHashCode();
    }

    public HashClass(Vector2 vt, Vector3 vn)
    {
        vertex = defaultvector3;
        uv = vt;
        normal = vn;

       // CalculateHashCode();
    }

    public HashClass(Vector3 v, Vector3 vn)
    {
        vertex = v;
        uv = defaultvector2;
        normal = vn;
        
      //  CalculateHashCode();
    }
    


    public override bool Equals(object other)
    {
        HashClass obj = (HashClass)other;
        //Debug.Log("obj.vertext : " + obj.vertext + "  obj.uv : " + obj.uv + "   obj.normal  :  " + obj.normal);
        //Debug.Log("Equals : " + (obj.vertext == this.vertext && obj.uv == this.uv && obj.normal == this.normal));
        return (obj.vertex == this.vertex && obj.uv == this.uv && obj.normal == this.normal);
    }
    private void CalculateHashCode()
    {
        string m =
            ((int)Math.Round(vertex.x)).ToString() +
            ((int)Math.Round(vertex.y)).ToString() +
            ((int)Math.Round(vertex.z)).ToString() +
            ((int)Math.Round(uv.x)).ToString() +
            ((int)Math.Round(uv.y)).ToString() +
            ((int)Math.Round(normal.x)).ToString() +
            ((int)Math.Round(normal.y)).ToString() +
            ((int)Math.Round(normal.z)).ToString();

        m = m.Replace("-", "");
        try
        {
            returnHashCode = int.Parse(m);

        }
        catch(Exception e)
        {
            Debug.Log("returnHashCode : " + returnHashCode);
            Debug.Log("m : " + m);
            //Debug.Log("this.vertext : " + this.vertext + "  this.uv : " + this.uv + "   this.normal  :  " + this.normal);
            //Debug.Log("(int)Math.Round(vertext.x)).ToString() : " + ((int)Math.Round(vertext.x)).ToString() + "    ((int)Math.Round(vertext.y)).ToString() : " + ((int)Math.Round(vertext.y)).ToString() + "    ((int)Math.Round(vertext.z)).ToString() : " + ((int)Math.Round(vertext.z)).ToString());
            
            
            oobj.SetActive(true);
            
        }



    }
    public override int GetHashCode()
    {
        return (vertex, uv, normal).GetHashCode();
    }
}