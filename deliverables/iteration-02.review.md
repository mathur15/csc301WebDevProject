# Re-Lec/Som and Co.


Video Link: https://www.youtube.com/watch?v=G0lGTX-71uQ

## Iteration 02 - Review & Retrospect

 * When:Thursday, March 8th
 * Where: BA2270 / BA3200


One person was unable to make it in person but sat in through video call.
We discussed what we have accomplished so far and talked about what we were not able to complete during this iteration. By doing this we were able to decide what features we would demonstrate in our video and plan what we would want completed in the next iteration.
As a team we went through this document and completed all parts of the form.


## Process - Reflection

For this iteration we encountered a couple of unexpected problems. Our main problem was figuring out how to send videos from the phone app to a database and have our web application access the video. Other than this, we were able to complete our goals of setting up the main feature of the phone app and web app. We found that meeting in our subgroups and coding together was really helpful as opposed to communicating online; we were able to help each other out and debug code easily.

#### Decisions that turned out well

Organizing large group meetings amongst all team members proved to be quite challenging (since everyone had busy schedules), so the choice to divide ourselves amongst the different subgroups was turned out well. Organizing ourselves into subgroups also allowed our team members to focus on individual strengths. For example, the members of our Unity development team had little experience coding using html and javascript whereas everyone else had little experience working with Unity.

*Using the Cobalt API, while currently slightly outdated, proved to be successful for finding out current classes based on location.
	*We use current location to find the current UofT building we are, and then use this current time to find what classes are starting soon in this building, that way we can display classes to the user to make the process of choosing a class a lot easier, and helps for verifying videos of users.
	*Link to the Cobalt API: https://cobalt.qas.im/

* Using Cloud9 turned out to be successful as we were easily able to run our code without having troubles setting up the database. Since it is also collaborative, we were able to make small adjustments and debug code much quicker versus constantly committing on git. It was also great that we were able to clone our repository on Cloud9 as it allowed us to work on remotely and pull any of our changes directly onto Cloud9. 


#### Decisions that did not turn out as well as we hoped


*Hosting the video on the webapp was not as easy as we thought, since we thought we would be sending the the video straight from the mobile Unity app to the web app.
	*Instead we are looking into other solutions for this, current possibility being the YouTube API, which allows for files up to 128GB and has a lot of support from different platforms.
	*Basic support for Unity was not found but it should be possible to find a workaround to uploading via HTTP request and sending bytes, something to look into.

*Recording on the Unity app was harder to do on mobile than expected, current solution is pretty complex and requires a lot of binding of different assets. Will look into a better solution in the future.
	* Current recording has a lot FPS and compiling the video has a long wait time due to the nature of compiling screenshots into an mp4 file via the FFmpeg codec
*It also limits other camera functionality we have planned for, may have to resort to native camera functionality for recording, but we will continue to look into better and more robust solutions.

* User authentication was much harder than we thought as we had to learn about Shibboleth which no one was familiar with. As we were reading about it, we became confused with installing and configuring the Shibboleth service provider. We plan on seeking help in the future to figure out how we can get user authentication working with our product.

Our idea to use three different branches to work on was not that useful this iteration.
* The routes for the site were made before the front end implementation occured. This lead to no overlapping work between the frontend and the backend. Additionally the files commited by the unity team had no merge conflicts with both other subgroups

#### Planned changes

*We are going to have more group coding sessions. As the project is coming along, the work is getting more difficult. We think that having more group coding sessions will help us work better.

*We will discuss the possibility of changing the days that we have coding sessions to maximize the amount of people we can have working at the same time. This way we can have better communication and more efficiency.

## Product - Review


#### Goals and/or tasks that were met/completed:


*Script for this iterations? video can be found in the shared Google Drive folder we use for our deliverables: https://drive.google.com/open?id=1mpUOrczHcgpnn76djn3Y2cR91xIJo2llxfUHFNXSM1o

* We have a navigable website with:
* a main search page
* a search results page
* a video watching page
* a ?Purchased Lectures? page

The decision to use sketchapp turned out to be very useful. Laying out the website on sketchapp made implementing the site much more streamlined.
https://puu.sh/zypli/1841878092.png
https://puu.sh/zypnR/1d5863d592.png
https://puu.sh/zyps9/2d00861f7b.png
https://puu.sh/zypt7/edfa9709f2.png
https://puu.sh/zFDFz/1ad4b3cc69.png

*Functionality on the video watching page was not part of our initial plan. It fetches a url from the server and displays the video. The video can be paused and 

* We have come up with a very basic credit system. Every user starts with two or three credits and they get a credit whenever they upload a video. Users must use one credit to gain access to a video lecture.  This is meant to incentivise users to contribute. We may improve or may not improve it later (depends on what more ideas we can come up with).

*Mobile app can record video and save it locally on the phone.
	*There are currently some issues with recording FPS and recording audio, this will be fixed during the next iteration.
	*Audio can be separately recorded, but the issue is combing the recorded video .mp4 file and the .mp3 audio file.
	*Using the Cobalt UofT API for gathering class codes around the user is working and just needs a UI for displaying to the user.
		*Nearby classes UI (backend done, simple UI done) --> 
*Recording UI (backend done, extremely basic UI done) --> 
*Recording (basic functionality mostly done)--> 
*Saving and Uploading (saving video and audio ((not combined)) done, uploading not done)
	*Rudimentary APK file with basic feature showcase: https://drive.google.com/open?id=1vX-4gJr7WGR3wTzMHhaNK8VrFpcdZ7ez

#### Goals and/or tasks that were planned but not met/completed:


*File transfer from the mobile app to the server. It was not completed because we needed to find an application that could compress the multimedia files to send to the server. We have that now so we will work on that for the next iteration.

*We planned to implement an API to authenticate users using their utoriids. This was not implemented because we did not find a simple api to achieve this objective. We will continue to look for an api to authenticate users. We might try redirecting from the uoft weblogin site.

*For the Unity mobile app side of things, we planned on implementing a rating system for video using the phone's accelerometer but because of issues implementing the video recording, we decided to move that to a future iteration. Depending on the speed at which we fully complete video and audio capture / compilation, we will then move on to rating the user-recorded videos.

## Meeting Highlights

Going into the next iteration, our main insights are:

*Starting on the video + review deliverable sooner than this iteration.
	*This would allow us to slowly fill in the required information throughout the whole time span and spend more time on each part of the deliverable.

 * Being able to upload videos to a website such as YouTube through the mobile app and accessing them in our web application.
	*https://developers.google.com/youtube/v3/code_samples/dotnet#upload_a_video

*Having basic UI and and solid user flow in the mobile app.
	*Main menu with mock-up login screen
	*Class selection screen
		*Display building they are in
		*Display classes starting soon
		*Allow user to input their own class
	*Record screen
		*Start / Stop Record + UI info
	*Upload screen
		*Progress for video saving / use other method for recording
*Progress on screen + wait-time
*Fully implement video and audio recording to .mp4, either using current method, or if the process proves to be too slow, we can switch to using the phones basic camera and give up freedom of control and the ability to display extra information to the user.

* Cleaning up the UI for the web application
* Extracting videos from a source (e.g. YouTube) and allowing videos to be viewed form our web application 
