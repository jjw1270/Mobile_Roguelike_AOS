using System;
using UnityEngine;
using UnityEngine.UI;
using ArduinoSerialAPI;

public class manager : MonoBehaviour
{
    // Start is called before the first frame update

    SerialHelper helper;

	public Text text;

    void Start()
    {
		try{
			helper = SerialHelper.CreateInstance("/dev/tty.usbmodem14601");
			helper.setTerminatorBasedStream("\n"); //delimits received messages based on '\n' char
			// helper.setLengthBasedStream();

			/*
			will received messages based on the length provided, this is useful in transfering binary data
			if we received this message (byte array) :
			{0x55, 0x55, 0, 3, 'a', 'b', 'c', 0x55, 0x55, 0, 9, 'i', ' ', 'a', 'm', ' ', 't', 'o', 'n', 'y'}
			then its parsed as 2 messages : "abc" and "i am tony"
			the first 2 bytes are the length data writted on 2 bytes
			byte[0] is the MSB
			byte[1] is the LSB

			on the unity side, you dont have to add the message length implementation.

			if you call helper.SendData("HELLO");
			this API will send automatically :
			 0x55 0x55    0x00 0x05   0x68 0x65 0x6C 0x6C 0x6F
			|________|   |________|  |________________________|
			 preamble      Length             Data

			
			when sending data from the arduino via usb cable, there's no preamble added.
			this preamble is used to that you receive valid data if you connect to your arduino and its already send data.
			so you will not receive 
			on the arduino side you can decode the message by this code snippet:
			char * data;
			char _length[2];
			int length;

			if(Serial.avalaible() >2 )
			{
				_length[0] = Serial.read();
				_length[1] = Serial.read();
				length = (_length[0] << 8) & 0xFF00 | _length[1] & 0xFF00;

				data = new char[length];
				int i=0;
				while(i<length)
				{
					if(Serial.available() == 0)
						continue;
					data[i++] = Serial.read();
				}


				...process received data...


				delete [] data; <--dont forget to clear the dynamic allocation!!!
			}
			*/
			

			helper.OnConnected +=  () => {
				Debug.Log("Connected");
				text.text = "Connected";
			};

			helper.OnConnectionFailed += () => {
				Debug.Log("Failed");
				text.text = "failed";

			};

			helper.OnDataReceived += () => {
				text.text = helper.Read();
			};

			helper.OnPermissionNotGranted += () => {
				//....
			};

			helper.Connect();
		}catch(Exception ex){
			text.text = ex.Message;
		}
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
	{
		if(helper!=null)
			helper.DrawGUI();
		else 
		    return;


		if(!helper.isConnected())
		if(GUI.Button(new Rect(Screen.width/2-Screen.width/10, Screen.height/10, Screen.width/5, Screen.height/10), "Connect"))
		{
			// if(helper.isDevicePaired())
				helper.Connect (); // tries to connect
			// else
			// 	sphere.GetComponent<Renderer>().material.color = Color.magenta;
		}

		if(helper.isConnected())
		if(GUI.Button(new Rect(Screen.width/2-Screen.width/10, Screen.height - 2*Screen.height/10, Screen.width/5, Screen.height/10), "Disconnect"))
		{
			helper.Disconnect ();
			// sphere.GetComponent<Renderer>().material.color = Color.blue;
		}

		if(helper.isConnected())
		if(GUI.Button(new Rect(Screen.width/2-Screen.width/10, Screen.height/10, Screen.width/5, Screen.height/10), "Send 'A' text"))
		{
			helper.SendData("A");
		}
	}

	void OnDestroy()
	{
		if(helper!=null)
		    helper.Disconnect ();
	}
}
