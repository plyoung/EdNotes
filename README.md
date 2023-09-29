# Unity Editor Notes

A simple tool to add notes to GameObject in the **Hierarchy** or Objects in the **Project** panel. The notes data in the scene are kept in a hidden object tagged as `EditorOnly` so the data will not end up in your final builds. The data concerning project objects are kept in an asset next to the this tool's scripts by default.

Open the Note panel via menu: `Window > Notes`. Enter some text and press [Save].

Click on the gear icon to show the Editor Notes settings where you can change the icon to use to indicate that an object has a note attached.

- Label Expand: if you choose a Label icon then this determine if the colour expands full width or only object name length.
- Icon Position: select where icon should be shown if using icon type indicator.
	+ Left/Right edge of panel
	+ To Left/Right of the object's name
- Icon Offset: can be used to move the icon from the selected position

![screenshot](https://user-images.githubusercontent.com/837362/30640573-bb962954-9e03-11e7-88e9-1d03f2379195.png)



## Display Note in Inspector

To display the note in the Inspector pane when you click on a game object in the Hierarchy, Add Component of type `ShowEditorNote`.

![editor_screenshot](https://github.com/mhardy/EdNotes/assets/115857/3acffcce-3f9b-4781-8bd7-f5aad04f80f2)


## Installation

- Click on Code > Download Zip

- Copy the folder : `Assets\EdNotes` to your `Assets` folder



