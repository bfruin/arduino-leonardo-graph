arduino-leonardo-graph
======================

This application visualizes a temporal representation of Analog In/Out and Digital In/Out 
of an Arduino Leonardo by reading from the serial port specified by PORT_NUM using
[Dynamic Data Display 2.0](http://dynamicdatadisplay.codeplex.com/) for wpf.
  
Modify your Arduino code to output to the serial in the form:
*A#,#,...,#,#;
*D#,#,...,#,#;
 
Where A denotes Analog and D denotes Digital and each # represents
the reading where In/Out alternate in order of the port number.
Note that this currently only supports devices with up to the same
number of ports as a Arduino Leonardo due to screen space, but future
versions will support more variable number of ports.
