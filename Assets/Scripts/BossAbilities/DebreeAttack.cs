using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

struct square{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public square(float _minX, float _maxX, float _minY, float _maxY) : this()
    {
        minX = _minX;
        maxX = _maxX;
        minY = _minY;
        maxY = _maxY;
    }
}


/* Splits boss room into 9 sections then spawns 8 debree in each section
 * This way its a bit less random and the debree is more evenly spaced out
 */

public class DebreeAttack : MonoBehaviour
{
    GameObject debree;

    square roomCoords;

    square[] sections = new square[9];

    void Start()
    {
        var addressable = Addressables.LoadAssetAsync<GameObject>("Debree");
        addressable.Completed += (obj) => debree = obj.Result;
        
    }
    public void Use()
    {

        int count = 0;
        for(int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                float minX = roomCoords.minX + i * ((roomCoords.maxX - roomCoords.minX) / 3);
                float maxX = roomCoords.minX + (i + 1) * ((roomCoords.maxX - roomCoords.minX) / 3);
                float minY = roomCoords.minY + j * ((roomCoords.maxY - roomCoords.minY) / 3);
                float maxY = roomCoords.minY + (j + 1) * ((roomCoords.maxY - roomCoords.minY) / 3);
                sections[count++] = new square(minX, maxX, minY, maxY);
            }
        }
        for(int i = 0; i < 9; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                float x = Random.Range(sections[i].minX, sections[i].maxX);
                float y = Random.Range(sections[i].minY, sections[i].maxY);

                GameObject debreeObject = Instantiate(debree, new Vector3(x, y, 1), transform.rotation);
            }
        }

    }

    public void setCoords(float minX, float maxX, float minY, float maxY)
    {
        roomCoords = new square(minX, maxX, minY, maxY);
    }
}
