using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingBlades_Controller : MonoBehaviour
{
    private Transform centerObject;  // Đối tượng trung tâm (player)
    private GameObject blade;  // kiếm sẽ xoay quanh player
    private int numberOfBlades;
    private float rotationSpeed;  // Tốc độ xoay (độ mỗi giây)
    private float radius;  // Bán kính vòng tròn mà các kiếm xoay quanh
    private int damage;
    private float percentExtraDamageOfSkill;
    private GameObject[] rotatingObjects;  // Mảng các kiếm sẽ xoay quanh player


    public void SetDancingBlades(Transform _centerObject, GameObject _blade,int _numberOfBlades, float _rotationSpeed, float _radius, int _damage, float _percentExtraDamageOfSkill)
    {
        centerObject = _centerObject;
        blade = _blade;
        numberOfBlades = _numberOfBlades;
        rotationSpeed = _rotationSpeed;
        radius = _radius;
        damage = _damage;
        percentExtraDamageOfSkill = _percentExtraDamageOfSkill;
        rotatingObjects=new GameObject[numberOfBlades];
        // GameObject newBlade = Instantiate(blade, transform.position, Quaternion.identity);

        for (int i = 0; i < numberOfBlades; i++)
        {
            GameObject newBlade = Instantiate(blade, transform.position, Quaternion.identity);
            newBlade.transform.parent = this.transform;
            newBlade.GetComponent<DancingBlades>().SetBlade(damage, percentExtraDamageOfSkill);
            rotatingObjects[i] = newBlade;
        }
        for (int i = 0; i < numberOfBlades; i++)
        {
            if(rotatingObjects[i]==null){
                Debug.Log(i+"null");
            }
        }

        // Tính toán vị trí ban đầu cho mỗi kiếm trong mảng rotatingObjects
        int numObjects = rotatingObjects.Length;
        for (int i = 0; i < numObjects; i++)
        {
            float angle = i * (360f / numObjects);  // Tính toán góc phân bố đều
            Vector2 newPosition = new Vector2(
                centerObject.position.x + radius * Mathf.Cos(Mathf.Deg2Rad * angle),
                centerObject.position.y + radius * Mathf.Sin(Mathf.Deg2Rad * angle)
            );
            rotatingObjects[i].transform.position = newPosition;  // Gán vị trí mới cho kiếm
        }




        
    }

    void Update()
    {
        // Xoay các kiếm quanh centerObject trong không gian 2D (trục Z)
        if (centerObject != null)
        {
            foreach (var obj in rotatingObjects)
            {
                // Xoay kiếm quanh player (trung tâm)
                obj.transform.RotateAround(centerObject.position, Vector3.forward, rotationSpeed * Time.deltaTime);

                // Tính toán hướng của đầu mũi kiếm
                Vector2 direction = ((Vector2)obj.transform.position - (Vector2)centerObject.position).normalized;

                // Tính góc quay cần thiết để đầu mũi kiếm luôn hướng ra xa player
                float angleToFace = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // Cập nhật góc quay của kiếm sao cho nó luôn hướng ra xa player
                obj.transform.rotation = Quaternion.Euler(0f, 0f, angleToFace);
            }
        }
    }

    
}
