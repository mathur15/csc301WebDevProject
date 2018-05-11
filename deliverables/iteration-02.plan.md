# Re-Lec/Som and Co.

## Iteration 02

 * Start date: Feb. 14th 2018 
 * End date: March 7th, 2018

## Process

For this iteration, we plan on meeting up at the beginning of each week (after Monday tutorials) to set deadlines and goals that should be achieved before the beginning of next week. At the end of each week, we will give each other updates on our progress to ensure that we will meet our goals and deadlines by our next meeting or tweak some changes to them if some goals and deadlines cannot be met. During this whole iteration, a group chat will be used to address any issues or concerns that could not be addressed during our in-person meetings.

In our first week we plan on deciding the general layout of our product and rank the importance of features that should be included in our product to prioritize what should be for this iteration.

In our second week and third weeks, we plan on developing our product so that it would be functional with minimal features.

#### Roles & responsibilities

During our planning meeting we divided up our team into three developer subgroups: front-end developers, back-end developers, and mobile developers. Each subgroup will be responsible for different things.

* Front-End Web Developers (Pravir and Manak) will be responsible for the websites client-facing code and architecture. Their responsibilities will be to:
	* Design the site using animations and graphics.
	* Incorporate functional interactive elements on the site such as menus, buttons and search bars
	* Ensure that communication to the server works

* Back-End Developers (Chad, Kevin, Som) will be responsible for:
* Creating routes for the website	
* Store and maintain multimedia files
	* User authentication and authorization (through the use of apis)

* Mobile Developers (Alexander, Martin) are responsible for the mobile component of the project. The mobile developers will be responsible for creating an app that :
	* Is interactive and intuitive
	* Captures pictures, videos, and audio
* Sends the multimedia files to the server
* Can display and check UofT courses based on GPS and time data

Additionally, a few more non-coding related roles will be filled:
* Overall Coordinator (Som) who is responsible for:
	* settling disputes
	* setting deadlines and ensuring goals are met

* Recorder (Som) who is responsible for:
*writing down notes	
*keeping track of artifacts

* Video Editor (Chad):
* Responsible for developing a script for the video and editing it so that it demonstrates our product	

#### Events


Official group meetings will be held twice a week: once during tutorial and once more on Friday. The purpose of these meetings are to go over the overall progress, bring up ideas, and address concerns. These meetings will also be a good time to discuss functionality between different subgroups (e.g. discuss mandatory routes needed on the site or how to transfer multimedia files from the app to the server).

Each subgroup will have their own coding sessions and code review sessions. These sessions are meant to just be developing code. The code review sessions will hopefully improve code quality and readability. Tentatively, the days for the coding and review sessions are:

SubGroup | Coding Sessions | Code Review Sessions
------------- | ------------- | -------------
Front End | Sunday | Wednesday
Back End | Monday | Thursday
Unity  | Monday | Thursday



A Facebook group chat has also been created so that immediate concerns may be addressed.


#### Artifacts


* The team will be in touch through a group chat to update the members of any changes made
* The group chat is active everyday and any changes are broadcasted immediately to the rest of the group, so the state of the app is always accurately known by all members
* Meetings will take place whenever necessary to handle any discrepancies
* Each subgroup have (minimum) weekly meetings to address the current progress on our weekly goals, what we need to have done by the next week, what our progress is like to reach our goals by the next deliverable
* Based on the features we are trying to implement for our application, 
* Tasks will also be assigned to based on the comfort level members have with every task.
* Each subgroup has a member that takes more of leadership position, therefore helping delegate tasks to the other members (taking in account skill levels and preferences)


#### Git / GitHub workflow

We will use three separate branches, one for each subgroup. The branches will be called:
* Web Front-End,
* Web Back-End, and 
* Unity

While developing a new feature, each subgroup will work on their own branch. Each subgroup will merge onto the main branch only after their feature is working. This will keep unfinished changes away from our production environment. Also it would prevent subgroups having to pull another subgroup?s changes when it does not immediately affect the feature they are working on. 

Merges onto the main branch must be approved by everyone in the subgroup. This will ensures two things: that everyone in the subgroup is satisfied with the new feature, and that the feature is fully completed.



## Product

#### Goals and tasks

Our goals for this iteration are to:
* Have basic functionality on the website. This will involve work on:
*graphic design and layouts
*front end development
*urls and routes
*RESTful commands to update the site in the background

* Come up with a credit system
	*We have do not have a solid idea of what to do, but it will involve mostly backend implementation

* Have a mobile app that can capture video, audio, and images. This will involve:
	* Use average movement from accelerometer screen resolution to generate a quality rating
	* Audio input data -> Route to scene for recording
Might be useful: https://support.unity3d.com/hc/en-us/articles/206485253-How-do-I-get-Unity-to-playback-a-Microphone-input-in-real-time-
	
* Have the mobile app upload multimedia files to the server
* Find and implement an API for user authentication using UtorIDs
	* UserID, Video File, QualityRating, Sucess/Fail
	* UofT API (https://github.com/cobalt-uoft/cobalt) To Check Current Class


#### Artifacts

* We plan on using AWS Cloud9 which is a cloud-based IDE that allows us to collaboratively write and run code. Since Cloud9 comes with essentials tools that support programming languages such as JavaScript, it will help us run and test our product without actually having a server. It also allows for easy database setup which our application will need to store our recorded lectures.
* For our frontend, we plan on using sketchapp as mock ups for how our web application will look like so that both the frontend and backend developers of our group know what features to implement.
* We will create a video that will show our problem space and how our product will address the issue. It will also show the basic UI of our product.
*An APK will be built and uploaded to the Google Drive to showcase the basic course verification and video recording features.
