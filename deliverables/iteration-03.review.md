# Re-Lec/ Som and Co.


[Deliverable 3 Video](https://youtu.be/XlGwwj8XGc4)

## Iteration 03 - Review & Retrospect

 * When: April 1st 3pm - 5pm
 * Where: 2210 BA

## Process - Reflection

For this iteration we encountered many major bugs and other issues that caught us by surprise. One of the major bugs was that we could not query our lecture information properly, but this turned out to be an easy fix. More importantly, we spent a long time trying to figure out how to integrate utorauth into our project without success. We decided to stop using utorath and use our own middleware authentication to demo our app. Additionally, meeting up proved to more difficult than previous iterations. All of us had less time to meet up in person and most of our meetings had to be rescheduled and moved online.

#### Decisions that turned out well

List process-related (i.e. team organization) decisions that, in retrospect, turned out to be successful.

* Having group coding sessions were very helpful. Just like last iteration, group coding sessions allowed us to easily organize work amongst the different subgroups. However, there was one major change from the last iteration. This was the fact that the meetings between the front end and the back end teams were held together. This far along into our project, changes made to either the front end and back end would involve work on the other end. 

* Our decision to use mlab to host our database online turned out to be successful. By hosting our database on mlab, everyone had accessed to use a unified and complete dataset without many logistical problems. Since everyone had access to the same dataset, it was easier to debug code. This saved us a nice quality of life change and allowed us to spend more time focusing on various other glaring issues that needed to be fixed such as improving functionality and video uploads.

#### Decisions that did not turn out as well as we hoped

List process-related (i.e. team organization) decisions that, in retrospect, were not as successful as you thought they would be.

 * 2 - 4 decisions.
 * Ordered from most to least important.
 * Feel free to refer/link to process artifact(s).


* One decision that did not work as well as intended was keeping the front end and the backend into two different branches. Near the end of the project, this lead to confusion and was often more trouble than it was worth. Since changes to the front end often relied on changes to the backend (and vise versa), it was a hassle to commit changes to both branches and push them separately. In hindsight it may have been better to get rid of this strategy and combine the two branches into one, especially since the group meetings for the front and back end groups were held together.

* Our choice to hold coding sessions in person did not work out as well as intended. Although the coding sessions were helpful, they were difficult to organize and people were oftentimes absent or we would have to reschedule. This is because we are nearing the end of the semester and everyone was busy working on interviews, and other projects and assignments. Nearing the end of the iteration, we began to hold these coding session online. These were far easier to organize, had a higher turnout, and were just as effective, if not more so than having them in person.

This is an image taken from one of the coding sessions:
https://docs.google.com/document/d/1AeNwTVRjq4fvYIR3LsEea4NKHoSiWD7yKSjZ4n7pxw0/edit?usp=sharing

#### Planned changes

List any process-related changes you are planning to make (if there are any)


* Since this is the last iteration, we will not be making any process related changes. However, if we are to continue developing our app, we will stop keeping the front end and the back end branches separate. This far into the project, changes to the front end of the site will require adjustments to be made in the backend as well. This change will allow members of the front end and back end branch to add features without having the hassle of commiting and merging amongst multiple branches. From a technical aspect, this will not be hard ourselves since most of the members in the front end subgroup and the back end subgroup are comfortable working on front end and back end. 

* Near the end of the iteration, we started raising issues and assigning people to fix them (on GitHub). This was very useful in alerting people to the problems and the issues that needed to be resolved. Issues that were raised often resolved quickly. We did not plan on using issues but they were fairly useful. We will definitely use them more frequently if we continue working on the project. 

## Product - Review

#### Goals and/or tasks that were met/completed:


Since we had a lot of small goals and tasks that needed to be completed for this iteration, many of these goals will be grouped into a single point for discussion.

* We were also able to complete the video upload and video playing features. The video once uploaded gets compressed down to a more reasonable streamable size. The server also takes a screen capture from the video to be used as a thumbnail around the website.

* On the front end (and the back end), we were able to get the user to search for videos, watch videos, and login. These were the most important part of the project and we focused on making this work.

* On the unity end, we were able to accomplish all of our goals including:
	* creating a friendly UI and cobalt API
* login and registration
	* all video capturing components such as recording video, audio, and geographical data 
	* video uploading involving metadata, and file path extraction
* On the back end, we were able to accomplish most of our goals including:
	* user authentication (kind of - its more of a fake for the demo)
* video uploading
* sending the video to the site for the user to see
* search functionality
* upvotes functionality
* On the front end, we were able to:
	* create all the necessary buttons
	* create the search functionality
	* display the video
	* upvotes functionality


Image of our purchase button:
https://drive.google.com/file/d/18pm8lNFDyO4T9AM0ZjOHwKPDScHvnOcC/view?usp=sharing

#### Goals and/or tasks that were planned but not met/completed:


* utorauth - . Although spent a lot of time understanding how to use shibboleth and weblogin,  we were unable to create an authentic login system using utorids. When it came to shibboleth, we did not have the resources to set up our own authentication server. Additionally, we were unable to find documentation that could lead us through the setup (for both uoft weblogin). After spending many weeks on this feature, we decided to cut our losses and create our own user authentication system using middleware and encryption. This will give us a bare minimum working example that we can use to, at the very least, demonstrate how our app will function ideally.

* Video highlights - We initially pitched that we were going to have video highlights. This function would allow users to instantly recognize and skip to the most important parts of the lecture recordings. We were unable to implement this feature since no-one on the team had any experience doing something like this. With the short amount of time remaining, we decided to fine-tune the other features we have that work, instead of beginning development on a new feature (that would most likely not work).

* Comments - Although this feature was not too difficult to implement, we spent far too much time fixing bugs and trying to get utorauth working. We decided that creating user comments was not as important as the other features in our project and prioritized fine-tuning those features instead. Such features include: getting videos to play correctly, having videos upload correctly, having users be able to purchase videos, etc. However we did lay the groundwork for a comment system, setting up a schema to store comments and writing code in the back end to create comments.

## Meeting Highlights

Going into the next iteration, our main insights are:.

* On the process end, we will merge the front end and the back end branches together to make development more streamlined. This will allow for features to be committed in only one commit instead of multiple commits and merges amongst different branches. Additionally, we will be moving most of our coding sessions online since everyone’s schedules are becoming packed near the end of the semester. We believe that moving coding sessions online will be more convenient for everyone and lead to increased productivity.

* Our top priority is to get the autorauth to work authentically. Having a substitute for it is not an optimal solution and is only meant as a substitute for demonstration purposes. We believe that using utorids to login to our app is very important to students. Not only will this be more convenient with users, it will cement our app as an important part of our users daily learning routine.

* Our secondary objective is to augment our app with various additional (but necessary) features. This includes both comments and video highlights. Although these are not necessary for the core functionality of our app, we feel that these aspects are core to our vision for re-lec to bring the classroom experience online. 
