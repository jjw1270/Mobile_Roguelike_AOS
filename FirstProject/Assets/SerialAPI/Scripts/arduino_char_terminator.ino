void setup()
{
	Serial.begin(9600);
}

void loop()
{
	if (Serial.available() > 0)
	{
		Serial.print("You Wrote : ");
		Serial.flush();
		while (Serial.available() > 0)
		{
			Serial.write((char)Serial.read());
			Serial.flush(); //this is a must for the arduino to wait untill Serial.write is done
		}
	}

	delay(100);
	Serial.println("Send here any text");
}
