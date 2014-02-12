int ledPin = 9;    // LED connected to digital pin 9
int count = 0;

void setup()  {
  // nothing happens in setup
  Serial.begin(9600);
  analogWrite(ledPin, 255);
}
 
void loop()  {
  //print out the data!
  Serial.println(generateAnalog() + generateDigital());
  delay(10);   
}

String generateAnalog() {
  int sensorValue0 = analogRead(0);
  int sensorValue1 = analogRead(1);
  int sensorValue2 = analogRead(2);
  int sensorValue3 = analogRead(3);
  int sensorValue4 = analogRead(4);
  int sensorValue5 = analogRead(5);
  
  int sensorValueOut0 = map(sensorValue0, 0, 1023, 0, 255);
  int sensorValueOut1 = map(sensorValue1, 0, 1023, 0, 255);  
  int sensorValueOut2 = map(sensorValue2, 0, 1023, 0, 255);
  int sensorValueOut3 = map(sensorValue3, 0, 1023, 0, 255);
  int sensorValueOut4 = map(sensorValue4, 0, 1023, 0, 255);
  int sensorValueOut5 = map(sensorValue5, 0, 1023, 0, 255);
  
  return "*A" + String(sensorValue0) + "," + String(sensorValueOut0) + "," + 
  String(sensorValue1) + "," + String(sensorValueOut1) + "," + 
  String(sensorValue2) + "," + String(sensorValueOut2) + "," + 
  String(sensorValue3) + "," + String(sensorValueOut3) + "," + 
  String(sensorValue4) + "," + String(sensorValueOut4) + "," + 
  String(sensorValue5) + "," + String(sensorValueOut5) + ";";
}

String generateDigital() {
 int digital0 = digitalRead(0);
 int digital1 = digitalRead(1);
 int digital2 = digitalRead(2);
 int digital3 = digitalRead(3);
 int digital4 = digitalRead(4);
 int digital5 = digitalRead(5);
 int digital6 = digitalRead(6);
 int digital7 = digitalRead(7);
 int digital8 = digitalRead(8);
 int digital9 = digitalRead(9);
 int digital10 = digitalRead(10);
 int digital11 = digitalRead(11);
 int digital12 = digitalRead(12);
 int digital13 = digitalRead(13);
  
 return "*D" + String(digital0) + "," + String(digital0) + "," +
 String(digital1) + "," + String(digital1) + "," +
 String(digital2) + "," + String(digital2) + "," +
 String(digital3) + "," + String(digital3) + "," +
 String(digital4) + "," + String(digital4) + "," +
 String(digital5) + "," + String(digital5) + "," +
 String(digital6) + "," + String(digital6) + "," +
 String(digital7) + "," + String(digital7) + "," +
 String(digital8) + "," + String(digital8) + "," +
 String(digital9) + "," + String(digital9) + "," +
 String(digital10) + "," + String(digital10) + "," +
 String(digital11) + "," + String(digital11) + "," +
 String(digital12) + "," + String(digital12) + "," +
 String(digital13) + "," + String(digital13) + ";";
}
