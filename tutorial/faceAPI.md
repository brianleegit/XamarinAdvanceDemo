# FaceApi
We use [Microsoft Cognitive Services Face API](https://www.microsoft.com/cognitive-services/en-us/face-api/documentation/overview) in login function, following are some brief descriptions of this service.

## Group and Training
Before we start facing identification, we should tell the service about us. We create a group and send some picture with the name of the person in them.
![](https://i.imgur.com/6dps4sl.jpg)
(Image from [Face API Documentation](https://www.microsoft.com/cognitive-services/en-us/face-api/documentation/overview))

In our Xamarin demo project, we use the nuget package [Microsoft Cognitive Services Face API](https://www.nuget.org/packages/Microsoft.ProjectOxford.Face/).
First of all, we should initialize it with our **KEY**, you can get it on the Cognitive Service website.
```cs
FaceServiceClient fc = new FaceServiceClient("API KEY");
```
### Creating a group
We define the **Group ID** in Constants.cs
```cs
await fc.CreatePersonGroupAsync("Group ID", "Group Name");
```
### Add a person
Add a person in our grounp, and we will get a **Person ID** which will be used in the next step.
```cs
await fc.CreatePersonAsync("Group ID", "Person name")
```
### Add a person face
After we add a person, we now tell the service what the person looks like. Just upload a picture and binding to the person.
```cs
await fc.AddPersonFaceAsync("Group ID", "Person ID", "Picture URL")
```
### Training the group
When we finish adding persons and pictures, we will send a request to the service make it start training.
```cs
await fc.TrainPersonGroupAsync("Group ID");
```
Normally, the process should finish in few seconds. You can call following function to check training status.
```cs
await fc.GetPersonGroupTrainingStatusAsync("Group ID");
```
## Face Identification
### Detect
First, we need a unique face ID, just upload a picture ,the service will return face IDs and the areas of them.
```cs
 var faceResult = await fc.DetectAsync(photo.GetStream());
```
In this project, we reject the picture with more than one face.
### Identify
After we get the Face ID, we can identify it by sending the ID and Group Name.
```cs
var identyResult = (await fc.IdentifyAsync("Group ID", ids))[0].Candidates;
```
This result will contain the person data if he is in our group. And we can go the next step to identify person emotion and update the Azure server.