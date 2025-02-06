using System;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance;
    public Pin Pin;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        CreatePin();
    }

    public void CreatePin()
    {
        Pin newPin = Instantiate(Pin, new Vector3(0, -4, 0), Quaternion.identity);
        this.Pin = newPin;
    }

    // Update is called once per frame
    void Update()
    {
        // key controller
        if (Input.GetKeyUp(KeyCode.Space) && !Pin.IsThrow)
        {
            this.Pin.Throw();
        }
    }
}
