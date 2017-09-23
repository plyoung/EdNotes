# Unity Editor Notes

A simple tool to add notes to GameObject in the Hierarchy or Objects in the Project panel. The notes data in the scene are kept in a hidden object tagged as `EditorOnly` so the data will not end up in your final builds. The data concerning project objects are kept in an asset next to the this tool's scripts by default.

Open the Note panel via menu: `Window > Notes`. Enter some text and press [Save].

Click on the gear icon to show the Editor Notes settings where you can change the icon to use to indicate that an object has a note attached.

- Label Expand: if you choose a Label icon then this determine if the colour expands full width or only object name length.
- Icon Position: select where icon should be shown if using icon type indicator.
	+ Left/Right edge of panel
	+ To Left/Right of the object's name
- Icon Offset: can be used to move the icon from the selected position

[![Video on YouTube](http://www.plyoung.com/img/buttons/youtube_s.png)](https://www.youtube.com/watch?v=b_s6f5JvB9Q) 
[![Follow on Twitter](http://www.plyoung.com/img/buttons/twitter_s.png)](https://twitter.com/pl_young) 
[![Unity Asset Store](http://www.plyoung.com/img/buttons/assetstore_s.png)](https://assetstore.unity.com/publishers/380) 
[![Donate](http://www.plyoung.com/img/buttons/paypal_s.png)](https://www.paypal.me/plyoung) 
[![Patreon](http://www.plyoung.com/img/buttons/patreon_s.png)](https://www.patreon.com/plyoung) 

![screenshot](https://user-images.githubusercontent.com/837362/30640573-bb962954-9e03-11e7-88e9-1d03f2379195.png)

[![Video](https://img.shields.io/badge/-Video-e05d44.svg?style=flat-square&logo=data%3Aimage%2Fpng%3Bbase64%2CiVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAMAAAAoLQ9TAAAAP1BMVEUAAADeXkLgXETeX0XfXEThXUTiXUbgXUTgXUTfYEDgXUTgXkPmZk3gXUTfXUX%2FgIDhW0XgXkTgXUThXUTgXURvgtz7AAAAFHRSTlMANs9GQLgs%2FJoY830K5GACQ7Qpl%2Bh2g10AAABGSURBVHgBfc85FoBACATRcVzGBfe6%2F1mNqUDC%2Fx7QXbralzQDjJOANi8ZYN0iA%2BzHmQHqJYD7EdDe%2BAet6KjeKpiiq5zrfx6SCX%2Bogq05AAAAAElFTkSuQmCC)](https://www.youtube.com/watch?v=b_s6f5JvB9Q)
[![Twitter](https://img.shields.io/badge/-@pl__young-007ec6.svg?style=flat-square&logo=data%3Aimage%2Fpng%3Bbase64%2CiVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAMAAAAoLQ9TAAAAulBMVEUAAAAgn%2FQcovIdovMdoPAkpO0kkv8covMrqv8covMdofIdofIcovIcofEYnvMbofMdofIhpvQA%2F%2F8cofIcofIeofIbofIdofIcofIaou4dofIdofEeofIdofEdoPIeoPMdofIdofIdofIdofIdoPMeovIdoPEdofIdovMeoPIfofUdofIdofIVquogn%2B8eoPIeofIdofIcofMcovEdofMdofIdofIhpe8cn%2FEcofEeoPMbn%2FQAv%2F8dofL9CP3sAAAAPXRSTlMAGGNoIw4HkQY%2F7%2FiZvBVBxBcB4L1fJuNkHp272KhOK8H%2B2e0%2BrJT5a4kxscsMEDua%2FJA3uPZPH0hsZjAEzRmxHgAAAH9JREFUeAF1yNUSggAUANE1RBQDQxS7uzu8%2F%2F9bclEGXzgvO7NEi8UTSUhhpE1UJiti5fIFimKXgHJFVLWGI1JvuBrVBFriaXfE14VefyAhFxhKaIRn%2FHcmeIypJT8zBzWXwIKv5Up86w1qu9sfRB1N1Oks6mJfCdzuj%2BfrTZQPzs4dZmTFoZYAAAAASUVORK5CYII%3D)](https://twitter.com/pl_young)
[![PayPal](https://img.shields.io/badge/-Donate-dfb317.svg?style=flat-square&logo=data%3Aimage%2Fpng%3Bbase64%2CiVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAMAAAAoLQ9TAAAA21BMVEX%2F%2F%2F8AAP8AQIAAJJIAIIAAYJ8AVaoAMIAAV64AJ4kAJoYAJ4UAK4UAW60AJ4YAXK0AKocAKIcAKocAKocAXa4AWqsAYK8AX7AAKYcAKocAKocAKYYAYrIAKYgAZLMAKYgAKYcAZ7UAZ7YAKYcAa7gAKYcAbLkAbLkARpgAKYcAMY4AecMAKYcAKYcAgMgAgskAGGoAGWwAGm0AHXIAHnIAIXkAInoAInsAI30AJYAAJoIAKIUAKYcALH8AMYQAPpUAP5AAXqsAXq8AbroAfcYAf8cAgMgAhMsAhcy1oBy8AAAAMHRSTlMAAQQHCAgMECYnKC4wNTtLVVlbaGt3fYGEhpOWpq2ur8DCxtjZ2trc4uTk%2Bfz9%2Fv6U8Jh7AAAAlElEQVQYGQXBy0rDQBQA0DMz1yS11MZF0UX1%2F%2F9JcKP4gFQxgdiHTDwnod%2F3cBiHEwW7e3B9u%2FyQ0QDsNwRlhb9KKlcEXc%2FpAnbjJdOggvRwJ9NiAduylmlZKtx0zoKGmlJeRcskREezyeB5ELotJcPy9EpokHiZjuMRoUVi%2BASyxFcwA8LHd1o%2FepsAWf2dz%2B9mgH8fYCsaIQYkbQAAAABJRU5ErkJggg%3D%3D)](https://www.paypal.me/plyoung)
[![Patreon](https://img.shields.io/badge/-Patreon-f86754.svg?style=flat-square&logo=data%3Aimage%2Fpng%3Bbase64%2CiVBORw0KGgoAAAANSUhEUgAAABAAAAAPCAMAAADarb8dAAAAnFBMVEUAAAAHLkoLMk0QNlEXPFYaP1gaP1gfQ1wuUGc0VGs1VWw3V20%2FXXNEYndKZ3tie4xogJFvhpZ%2FlKKBlaOHmqiNn6yPoa2ZqbWquMGuu8S1wMnK0tjIXFHKXFHLXVHR2N3X3eHe4%2Bfn6u3o7O7r7vDs7%2FHw8%2FT2%2BPn6%2B%2Fv3%2BPn8%2Ff3%2B%2Fv70ZlP1Z1P3ZlT4Z1T5aFT8%2FP3%2B%2Fv7%2F%2F%2F9eHmIbAAAALHRSTlMAAwgNFRgaHy80NTY%2BRUxkanGDhYmPk56usbjN0tLS09rh6evw8fP4%2B%2Fz9%2FvLDJQkAAABxSURBVHgBbchFFsJAEADRAoK7uzsBAvT978ajMz3DIrWrz%2F2xAGjNjtfNIAKS1wpy44%2F82tUd9MW1LyqUzmKNFNri2yr0ArwVugEOCpXYw0SBqf2llkK0Fu3WIQUKw5NIPG9iAPlqowwGViY8kyX%2FfQGdNBqN0LDLVQAAAABJRU5ErkJggg%3D%3D)](https://www.patreon.com/plyoung)




