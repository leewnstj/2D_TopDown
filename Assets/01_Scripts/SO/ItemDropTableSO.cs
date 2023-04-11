using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Items/DorpTable")]
public class ItemDropTableSO : ScriptableObject
{
    public List<ResourceDataSO> DropList;
}
