# Unity Editor Notes

A simple tool to add notes to GameObject in the Hierarchy or Objects in the Project panel. The notes data in the scene are kept in a hidden object tagged as `EditorOnly` so the data will not end up in your final builds. The data concerning project objects are kept in an asset next to the this tool's scripts by default.

Right-click on an object in the Hierarchy or Project panel and choose "Notes", or `Alt+N`, to open the note window. Enter some text and press [Save].

Click on the gear icon to show the Editor Notes settings where you can change the icon to use to indicate that an object has a note attached.

- Label Expand: if you choose a Label icon then this determine if the colour expands full width or only object name length.
- Icon Position: select where icon should be shown if using icon type indicator.
	+ Left/Right edge of panel
	+ To Left/Right of the object's name
- Icon Offset: can be used to move the icon from the selected position
