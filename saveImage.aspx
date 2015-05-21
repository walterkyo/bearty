<%@ Page Language="C#" AutoEventWireup="true" CodeFile="saveImage.aspx.cs" Inherits="saveImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<script type="text/javascript" charset="utf-8">
    var myData = "";
    $(document).ready(function () {
        $("#getDataFromServer").click(function () {
            var imageData = myData;
            $.ajax({
                type: "POST",
                url: './saveImage.ashx',
                data: {
                    image: imageData
                },
                beforeSend: function () {
                    $("#comment2").text("Start ajax " + imageData.length);
                },
                success: function (data) {
                    $("#comment2").text("Uploaded! " + data);
                },
                error: function (request, error) {
                    $("#comment2").text("Error! " + error);
                }
            });
        });
    })

    function capturePhotoEdit(source) {
        navigator.camera.getPicture(onPhotoDataSuccess, onFail, {
            quality: 50,
            destinationType: destinationType.DATA_URL,
            sourceType: source
        });
    }

    function onFail(message) {
        alert('Failed because: ' + message);
    }

    function onPhotoDataSuccess(imageData) {

        console.log(imageData);

        var smallImage = document.getElementById('smallImage');

        smallImage.style.display = 'block';

        smallImage.src = "data:image/jpeg;base64," + imageData;
        myData = imageData;
        $("#comment").text(imageData.length);

    }
	</script>
<body>

    <form id="form1" runat="server">
    <div>
    <h1>Save Test</h1>
	
	<p>
		<a href="#" onclick="capturePhotoEdit(pictureSource.PHOTOLIBRARY);">get
			image</a>
	</p>
	<p>
		<a href="#" id="getDataFromServer">send image</a>
	</p>
	<span id="comment2"></span>
	<img style="display: none; width: 100px; height: 100px;"
		id="smallImage" src="" />
	<span id="imagename"></span>
	<span id="comment"></span>
    </div>
    </form>
</body>
</html>
