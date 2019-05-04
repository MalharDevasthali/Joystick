using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; //to get on Screen Events


public class VirtualJoyStick : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler
{ 
    //Implement three Interfaces and honce wirte three methods which catches eventdata for those 
    private Image BGImage;
    private Image Joystick;
    private Vector3 InputVec;
    // Start is called before the first frame update
    void Start()
    {
        BGImage = GetComponent<Image>(); //script is over Background only hence Getting just Image Componenet is Sufficient
        Joystick = transform.GetChild(0).GetComponent<Image>(); //jostick is 1st child of Backgrond hence we used 0 index to GetChild() method

    }
    public void OnDrag(PointerEventData eventData)
     {
        Vector2  Position;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(BGImage.rectTransform,eventData.position,eventData.pressEventCamera,out Position))

            //RectTransformUtility is class and ScreenPointToLocalPointInRectangle is static method for it
            //this static method returns a bool means returns true if the plane of the RectTransform is hit,
            //regardless of whether the point is inside the rectangle.In short when we click/touch over the rectangle
            //this method takes 4 parameters as input: 1)RectTransform of Rectagle is Image in this case
                            //                         2)position 
                            //                         3) Camera
                            //                         4) out position means it fills the variable
        {
            Position.x = (Position.x / BGImage.rectTransform.sizeDelta.x);//this is to get a position only from 0 to 1
            Position.y = (Position.y / BGImage.rectTransform.sizeDelta.y);
            //but we want the position from -1 to 1 for that we will do
            InputVec = new Vector3(Position.x * 2 + 1, 0, Position.y * 2 - 1);
            // this will only update the X's position but not y's bcoz this is not rectagular image
            //as it is circular we have to nomalize that vector
            InputVec = (InputVec.magnitude > 1.0f) ? InputVec.normalized : InputVec;
            //now we are ready to move joystick image
            Joystick.rectTransform.anchoredPosition = new Vector3(InputVec.x*(BGImage.rectTransform.sizeDelta.x/2),
                                                                  InputVec.z*(BGImage.rectTransform.sizeDelta.y/2));
            //here we are dividing size of BGImage to restrict the movement of Jostick to half of the rectangle

        }


        
     }
    public void OnPointerDown(PointerEventData eventData)
     {
        OnDrag(eventData);    //we are just redirected OnPointerDown to the Drag Method
     }

     public void OnPointerUp(PointerEventData eventData)
     {
        //this is responsible to set the Joystick back to its original position
        InputVec = Vector3.zero; ;
        Joystick.rectTransform.anchoredPosition = Vector3.zero;
     }
    public float Horizontal() //function responsible for horizontal movement of the Sphere
    {
        if (InputVec.x != 0)
            return InputVec.x;
        else
            return Input.GetAxis("Horizontal");
    }
    public float Vertical() //function responsible for vertical movement of the Sphere
    {
        if (InputVec.z != 0)
            return InputVec.z;
        else
            return Input.GetAxis("Vertical");
    }
}
