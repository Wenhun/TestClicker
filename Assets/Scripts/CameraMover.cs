using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] float speed = 2f;

    bool moveUp = false;
    bool moveDown = false;
    bool moveLeft = false;
    bool moveRight = false;

    void Update()
    {
        if(moveUp) transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
        if(moveDown) transform.Translate(Vector3.back * Time.deltaTime * speed, Space.World);
        if(moveLeft) transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
        if(moveRight) transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);

        MoveCamera(); //input for developerd
    }

       void MoveCamera()
    {
        float xValue = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float zValue = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(xValue, 0 , zValue, Space.World);
    }


    public void UpOn() {moveUp = true;}
    public void UpOff() {moveUp = false;}
    public void DownOn() {moveDown = true;}
    public void DownOff() {moveDown = false;}
    public void LeftOn() {moveLeft = true;}
    public void LeftOff() {moveLeft = false;}
    public void RightOn() {moveRight = true;}
    public void RightOff() {moveRight = false;}
}
