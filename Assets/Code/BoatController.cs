using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    [Header("Power State")] public bool isPowered;
    
    [Header("Movement Variables")] 
    [Range(-2,2)] public int leftWheelSpeed;
    [Range(-2,2)] public int rightWheelSpeed;
    [Range(0,50f)] [Tooltip("Speed modifier, the lower the number the slower the boat")]public float speedModifier;
    [Range(0,10f)] [Tooltip("Rotation modifier, the lower the number the slower the boat")]public float rotationModifier;
    public float _boatSpeed;
    public float _desiredBoatSpeed;
    public float _rotationSpeed;
    public float _desiredRotationSpeed;

    [Header("Visuals")] 
    public GameObject leftWheelModel;
    public GameObject rightWheelModel;
    public ParticleSystem leftWheelFrontParticle;
    public ParticleSystem leftWheelBackParticle;
    public ParticleSystem rightWheelFrontParticle;
    public ParticleSystem rightWheelBackParticle;
    //public TrailRenderer boatTrail;
    public TrailRenderer lwTrail;
    public TrailRenderer rwTrail;

    [SerializeField] private LeverLinkControl _leverLinkControl;
    [SerializeField] private LeverWheelControl leftWheelUI;
    [SerializeField] private LeverWheelControl rightWheelUI;
   

    private void Start()
    {
        leftWheelUI.OnWheelChange += ChangeLeftInput;
        rightWheelUI.OnWheelChange += ChangeRightInput;
    }

    private void Update()
    {
        //AttachLevers();
        ChangeBothInput();
        ChangeLeftInput();
        ChangeRightInput();
           
        if (rightWheelSpeed !=0 | leftWheelSpeed !=0 && isPowered)
        {
            CalculateSpeed();
            CalculateDirection();
            RotateWheels();
            
        }
        else
        {
            _boatSpeed = Mathf.Lerp(_boatSpeed, 0, Time.deltaTime * 0.5f);
            _rotationSpeed = Mathf.Lerp(_rotationSpeed, 0, Time.deltaTime * 0.5f);
            CalculateDirection();
        }
        
        BoatTrails();
        ChangeParticleState();
    }

    public void PowerBoatOn()
    {
        isPowered = true;
    }

    public void PowerBoatOff()
    {
        isPowered = false;
    }
    
    //Rotates the wheels visually
    private void RotateWheels()
    {
        var lwRotationSpeed = 0.1f * leftWheelSpeed ;
        var rwRotationSpeed = 0.1f * rightWheelSpeed ;
        leftWheelModel.transform.Rotate(Vector3.left, lwRotationSpeed,Space.Self);
        rightWheelModel.transform.Rotate(Vector3.right, rwRotationSpeed, Space.Self);
    }


    private void AttachLevers()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _leverLinkControl.LinkLevers();
        }
    }

    
    private void ChangeLeftInput(object sender, LeverWheelControl.OnWheelChangeEventArgs e)
    {
        leftWheelSpeed = e.sliderValue;
    }
    private void ChangeLeftInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            /*if (_leverLinkControl.isLeverLinked)
            {
                _leverLinkControl.LinkLevers();
            }*/
            leftWheelSpeed++;
            if (leftWheelSpeed > 2)
            {
                leftWheelSpeed = 2;
            }

            leftWheelUI.slider.value = leftWheelSpeed;
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            /*if (_leverLinkControl.isLeverLinked)
            {
                _leverLinkControl.LinkLevers();
            }*/
            leftWheelSpeed--;
            if (leftWheelSpeed < -2)
            {
                leftWheelSpeed = -2;
            }
            leftWheelUI.slider.value = leftWheelSpeed;
        }
    }
    
    private void ChangeRightInput(object sender, LeverWheelControl.OnWheelChangeEventArgs e)
    {
        rightWheelSpeed = e.sliderValue;
    }
    
    private void ChangeRightInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            /*if (_leverLinkControl.isLeverLinked)
            {
                _leverLinkControl.LinkLevers();
            }*/
            rightWheelSpeed++;
            if (rightWheelSpeed > 2)
            {
                rightWheelSpeed = 2;
            }
            rightWheelUI.slider.value = rightWheelSpeed;
            
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            /*if (_leverLinkControl.isLeverLinked)
            {
                _leverLinkControl.LinkLevers();
            }*/
            rightWheelSpeed--;
            if (rightWheelSpeed < -2)
            {
                rightWheelSpeed = -2;
            }
            rightWheelUI.slider.value = rightWheelSpeed;
        }
    }
    
    //Il faut regler le proble que quand les levier sont attache ca fait double coche
    private void ChangeBothInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            //_leverLinkControl.LinkLevers(true);
            
            /*
            if (rightWheelSpeed != leftWheelSpeed)
            {
                rightWheelSpeed = 0;
                leftWheelSpeed = 0;
            }
            else
            {
                rightWheelSpeed++;
                leftWheelSpeed++; 
            }
           */

            rightWheelSpeed++;
            leftWheelSpeed++;
            
            if (rightWheelSpeed > 2)
            {
                rightWheelSpeed = 2;
            }
            if (leftWheelSpeed > 2)
            {
                leftWheelSpeed = 2;
            }
            leftWheelUI.slider.value = leftWheelSpeed;
            rightWheelUI.slider.value = rightWheelSpeed;
            
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            /*_leverLinkControl.LinkLevers(true);
            

            if (rightWheelSpeed != leftWheelSpeed)
            {
                rightWheelSpeed = 0;
                leftWheelSpeed = 0;
            }
            else
            {
                rightWheelSpeed--;
                leftWheelSpeed--;
            }*/
            
            rightWheelSpeed--;
            leftWheelSpeed--;
            
            if (rightWheelSpeed < -2)
            {
                rightWheelSpeed = -2;
            }
            if (leftWheelSpeed < -2)
            {
                leftWheelSpeed = -2;
            }
            leftWheelUI.slider.value = leftWheelSpeed;
            rightWheelUI.slider.value = rightWheelSpeed;
        }
    }

    private void ChangeParticleState()
    {
        if (_boatSpeed != 0)
        {
            leftWheelFrontParticle.Play();  
            leftWheelBackParticle.Play();
        }
        else
        {
            leftWheelFrontParticle.Stop();
            leftWheelBackParticle.Stop();
        }
    }
    
    private void CalculateSpeed() //Takes the input of both wheel and creates a Vector2 where X is the leftWheel and Y is the rightWheel
    {
        _desiredBoatSpeed = (rightWheelSpeed + leftWheelSpeed) * (speedModifier /500) / 60f;
        //Calculate the direction of the spin
        switch (rightWheelSpeed)
        {
            case -2:
                switch (leftWheelSpeed)
                {
                    case -2:
                        _desiredRotationSpeed = 0f * ( rotationModifier / 500) * 2f;
                        break;
                    case -1 :
                        _desiredRotationSpeed = 5f * ( rotationModifier / 500) * 2f;
                        break;
                    case 0:
                        _desiredRotationSpeed = 10f * ( rotationModifier / 500) * 2f;
                        break;
                    case 1:
                        _desiredRotationSpeed = 15f * ( rotationModifier / 500) * 2f;
                        break;
                    case 2:
                        _desiredRotationSpeed = 20f * ( rotationModifier / 500) * 2f;
                        break;
                    default:
                        break;
                }
                break;
            case -1 :
                switch (leftWheelSpeed)
                {
                    case -2:
                        _desiredRotationSpeed = -5f * ( rotationModifier / 500) * 2f;
                        break;
                    case -1 :
                        _desiredRotationSpeed = 0f * ( rotationModifier / 500) * 2f;
                        break;
                    case 0:
                        _desiredRotationSpeed = 5f * ( rotationModifier / 500) * 2f;
                        break;
                    case 1:
                        _desiredRotationSpeed = 10f * ( rotationModifier / 500) * 2f;
                        break;
                    case 2:
                        _desiredRotationSpeed = 15f * ( rotationModifier / 500) * 2f;
                        break;
                    default:
                        break;
                }
                break;
            case 0:
                switch (leftWheelSpeed)
                {
                    case -2:
                        _desiredRotationSpeed = -10f * ( rotationModifier / 500) * 2f;
                        break;
                    case -1 :
                        _desiredRotationSpeed = -5f * ( rotationModifier / 500) * 2f;
                        break;
                    case 0:
                        _desiredRotationSpeed = 0f * ( rotationModifier / 500) * 2f;
                        break;
                    case 1:
                        _desiredRotationSpeed = 5f * ( rotationModifier / 500) * 2f;
                        break;
                    case 2:
                        _desiredRotationSpeed = 10f * ( rotationModifier / 500) * 2f;
                        break;
                    default:
                        break;
                }
                break;
            case 1:
                switch (leftWheelSpeed)
                {
                    case -2:
                        _desiredRotationSpeed = -15f * ( rotationModifier / 500) * 2f;
                        break;
                    case -1 :
                        _desiredRotationSpeed = -10f * ( rotationModifier / 500) * 2f;
                        break;
                    case 0:
                        _desiredRotationSpeed = -5f * ( rotationModifier / 500) * 2f;
                        break;
                    case 1:
                        _desiredRotationSpeed = 0f * ( rotationModifier / 500) * 2f;
                        break;
                    case 2:
                        _desiredRotationSpeed = 5f * ( rotationModifier / 500) * 2f;
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                switch (leftWheelSpeed)
                {
                    case -2:
                        _desiredRotationSpeed = -20f * ( rotationModifier / 500) * 2f;
                        break;
                    case -1 :
                        _desiredRotationSpeed = -15f * ( rotationModifier / 500) * 2f;
                        break;
                    case 0:
                        _desiredRotationSpeed = -10f * ( rotationModifier / 500) * 2f;
                        break;
                    case 1:
                        _desiredRotationSpeed = -5f * ( rotationModifier / 500) * 2f;
                        break;
                    case 2:
                        _desiredRotationSpeed = 0f * ( rotationModifier / 500) * 2f;
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }

        _boatSpeed = Mathf.Lerp(_boatSpeed, _desiredBoatSpeed, Time.deltaTime * 0.5f);
        _rotationSpeed = Mathf.Lerp(_rotationSpeed, _desiredRotationSpeed, Time.deltaTime * 0.5f);
    }

    private void CalculateDirection() // Takes the overallSpeed and add the right force (foward/backward & rotation)
    {
        transform.Translate(transform.forward * _boatSpeed, 0);

        transform.Rotate(Vector3.up, _rotationSpeed, Space.World);
        
    }

    private void BoatTrails()
    {
        if (_boatSpeed == 0)
        {
            //boatTrail.emitting = false;
            rwTrail.emitting = false;
            lwTrail.emitting = false;
        }
        else
        {
            //boatTrail.emitting = true;
            rwTrail.emitting = true;
            lwTrail.emitting = true;
        }
    }
}
