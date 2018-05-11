# Re-Lec / Som and Co.

## Iteration 3

 * Start date: March 12, 2018
 * End date: April 1, 2018

## Process

For this iteration we plan on meeting at the beginning of each week (before and/or after Monday tutorials) to set goals to be accomplished before each weekend. This will give us time over the weekend to integrate and merge each sub-teams’ work together. To successfully accomplish this, we also plan on meeting more frequently as an entire group (or at least all leaders from each sub-team). This is to ensure all members are aware of how certain features should be implemented or address any limitations or concerns about current implementation of features so that we can plan and progress with production of our application. A group chat will also be used to address any immediate issues or concerns.

In our first and second week, we plan on finishing production and implementing most features for our application so that in our third week we can focus on cleaning up any UI and testing our app’s functionality. 

#### Changes from previous iteration

*Logistics:*
*More strict on deadlines.
	*We tended to procrastinate on certain things such as not fully completing the plan in the beginning. Now we are both: delegating tasks to specific people so this leads to a bigger sense of duty, and by setting specific deadlines for each document / task we have to do.

*Developing Side:*
* A change to the website development and testing platform - we decided to use *mlab* for hosting our database to allow us to locally run and deploy our web app. 
  * Allows for more flexibility in terms of accessibility and space (since free space on c9 is limited to 2 GB).
* We are going to merge the coding sessions for the front end and the back end. This is because improvements to the front end will require work to be done on the back end and vise versa

*Work-flow:*
* Using branching and PR on Github.
  * To help organize all work done, and make sure everyone is up to date with the changes taking place (this workflow further expanded on in the GitHub workflow section).

#### Roles & responsibilities

* Front-End Web Developers (Pravir and Manak) will be responsible for the websites client-facing code and architecture. Their responsibilities will be to:
	* Design the site using animations and graphics.
	* Incorporate functional interactive elements on the site such as menus, buttons and search bars
	* Ensure that communication to the server works

* Back-End Developers (Chad, Kevin, Som) will be responsible for:
	* Modifying database schemas to make searching for videos more efficient
* Retrieving course and video information for the front end from the database
* Creating an endpoint in the backend to handle video retrieval and storage 
	* User authentication and authorization (through the use of apis)

* Mobile Developers (Alexander, Martin) are responsible for the mobile component of the project. The mobile developers will be responsible for creating an app that:
	* Start screen UI
	* Improve course selection screen UI
	* Finish / smoothen video recording 
	* Look into sending the recorded .mp4 files to the server

* Code Reviewers (Pravir, Kevin) are responsible for merging code in the frontend and backend parts of our web application (Our mobile developers will be responsible for reviewing each others code as their code will not conflict with code in the frontend and backend).
	* Reviewing code so that it is functional
	* Merging branches and resolving any conflicts

* Additionally, a few more non-coding related roles will be filled:
* Overall Coordinator (Som) who is responsible for:
* settling disputes
	* setting deadlines and ensuring goals are met

* Recorder (Som) who is responsible for:
* writing down notes	
    	* keeping track of artifacts

* Video Editor (Chad):
    	* Responsible for developing a script for the video and editing it so that it demonstrates our product	

#### Events

Team Meetings:

Day | Where | People Involved | Purpose
--- | --- | --- | ---
Mar. 12 | Bahen | Everyone | Discuss our goals for this iteration
Mar. 16 | Bahen | Everyone | Delegate tasks to individual team members
Mar. 18 | Online | Everyone | Discuss on how to start integrating each sub-team’s work
Mar. 19 | Bahen | Everyone | Start combining frontend, backend and mobile application
Mar. 23 | Bahen | Everyone | Update each other on progress
Mar. 25 | Online | Everyone | Merge and combine main functionalities
Mar. 26 | Bahen | Everyone | Discuss any final improvements we could make
Mar. 28 | Bahen | Everyone | Complete Review.md

Subgroup Meetings:

* Mondays - Frontend and Backend- In person at Bahen
	* Work on front end and back end
* Wednesdays - Frontend and Backend - In person at Bahen
	* Work on front end and back end
* Thursdays - Mobile (Alex, Martin) - In person at Bahen
	* Mobile app development and progress update

#### Artifacts

Screenshot of our Mlab Database:
https://drive.google.com/file/d/1tEMx7E2dwoqYTRGUS-wlZGpWorOR4fPg/view?usp=sharing

To Do List for process:
https://docs.google.com/document/d/1ZPpmJxV4bQ9pL1-G7jMLTG-_AiWcX8B8BDPJ9Xh5RtI/edit?usp=sharing

#### Git / GitHub workflow  

We will use three separate branches, one for each subgroup. The branches will be called:
* Web Front-End,
* Web Back-End, and 
* Unity

* While developing a new feature, each subgroup will work on their own branch. Each subgroup will merge onto the main branch only after their feature is working. This will keep unfinished changes away from our production environment. Also it would prevent subgroups having to pull another sub group’s changes when it does not immediately affect the feature they are working on. 

* Merges onto the main branch must be approved by everyone in the subgroup. This will ensures two things: that everyone in the subgroup is satisfied with the new feature, and that the feature is fully completed.

* We plan on using issues on github to keep tracks of tasks and to update each other on progress or notify each other on bugs that need to be fixed.

* Although we did not use our planned workflow in our last iteration, we will attempt to use it again as in our last iteration, we all simply pushed changes to the master branch and worked among our subgroups to resolve any conflicts easily.  We will continue to work among subgroups for this iteration but also use branching and PRs on Github for cleaner and more organized workflow as files and code started becoming more intricate.
	* This way everyone will also stay updated with the progress being pushed onto the master branch before it becomes an official edit
	* Allow for easy merging between frontend and backend teams
	* Code can be reviewed when doing pull requests from branches onto master

## Product

#### Goals and tasks

 Unity :
* Implementing a user friendly UI for the android end of the product
	* Cleaner implementation of the Cobalt API
	* implement a way to manually change geographical data to account for computing errors
	* Conserve date and course data throughout the recording 
* Implement a working camera recording scene
* Looking into different Unity asset to use for recording video and audio
* Extract file path from recorded video
* implement login/registration functionality for the app
* Implement file upload from android to remote server to then be processed and uploaded onto the website

Back end:
* Retrieve videos from the server and send them to the site at request
* Put videos into the server from the phone
* User authentication
* Search functionality
* Create backend for comments system. This includes:
	* schema
	* logic that allows comments to be saved

Front end:
* Use cookies to store session information (like user id, video information, etc)
* Create buttons that allow users to buy videos
* Display videos for user to watch
* let user upvote videos
* Let user read and make comments

#### Artifacts

To Do List for our product: https://docs.google.com/document/d/1gj9fw6Eyf-WqohUoVzRl2FGaHdyIrkS5tqhiVYwKTHk/edit?usp=sharing
