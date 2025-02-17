--
-- PostgreSQL database dump
--

-- Dumped from database version 12.0
-- Dumped by pg_dump version 12.0

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: course; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.course (
    id smallint NOT NULL,
    num smallint NOT NULL,
    title character varying(180),
    summary text,
    semester character varying(20),
    prefix character varying(10),
    nonprogramprereq text
);


ALTER TABLE public.course OWNER TO postgres;

--
-- Name: coursePreReq; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."coursePreReq" (
    course_id smallint NOT NULL,
    prereq_id smallint NOT NULL
);


ALTER TABLE public."coursePreReq" OWNER TO postgres;

--
-- Name: course_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.course ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.course_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: course_learningoutcome; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.course_learningoutcome (
    course_id smallint NOT NULL,
    learningoutcome_id smallint NOT NULL
);


ALTER TABLE public.course_learningoutcome OWNER TO postgres;

--
-- Name: learningoutcome; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.learningoutcome (
    id smallint NOT NULL,
    name character varying(200),
    description character varying(4000)
);


ALTER TABLE public.learningoutcome OWNER TO postgres;

--
-- Name: learningobjective_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.learningoutcome ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.learningobjective_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: learningoutcome_post; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.learningoutcome_post (
    here smallint NOT NULL,
    post smallint NOT NULL
);


ALTER TABLE public.learningoutcome_post OWNER TO postgres;

--
-- Name: learningoutcome_pre; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.learningoutcome_pre (
    here smallint NOT NULL,
    pre smallint NOT NULL
);


ALTER TABLE public.learningoutcome_pre OWNER TO postgres;

--
-- Name: skill; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.skill (
    id smallint NOT NULL,
    name character varying(400),
    learningoutcome_id smallint
);


ALTER TABLE public.skill OWNER TO postgres;

--
-- Data for Name: course; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.course (id, num, title, summary, semester, prefix, nonprogramprereq) FROM stdin;
84	1020	Public Speaking	This is a practical and general course designed for students who desire to improve their speech efficiency, poise and self-confidence in public address situations. Special emphasis is placed on preparing, selecting, researching, organizing and delivering oral messages as well as on analyzing and evaluating the speaking-listening process.	Yr4 - Spring	COMM	
85	1400	Programming Fundamentals	The following topics will be covered in this course: Introduction to computers and programming; Data representation; Control structures; Functions; Arrays; Social context of computing.	Yr0-PreReq	CS	
86	1405	Programming Fundamentals Lab	This laboratory provides the hands-on experience necessary to begin to develop correct programming practices. It introduces the student to an integrated development environment. It provides the opportunity to apply software fundamentals in an appropriate programming language.	Yr0-PreReq	CS	
87	1410	Object-Oriented Programming	The following topics will be covered in this course: Encapsulation; Inheritance; Polymorphism; Exception handling. Basic data structures; Recursion; The software development process	Yr1 - Fall	CS	
88	1415	Object-Oriented Programming Lab	This laboratory provides continued experience to develop in depth correct programming practices. It provides the opportunity to apply object-oriented programming concepts and data structures.	Yr1 - Fall	CS	
89	1810	Web Development I	The course provides a technical overview of the Internet environment and the structure of the World Wide Web. The technical segment will focus on the design and implementation of an effective web site at the introductory level.	Yr3 - Fall	CS	
90	1820	Web Development II	CS 1820 introduces the principles of back end web development. The backend of a web application is an enabler for a front end experience. Backend developers need to understand programming languages and database and they must have an understanding of server architecture.	Yr3 - Spring	CS	
91	2420	Data Structures and Algorithms	The following topics will be covered in this course: Data abstration; Linked lists; Stacks and queues; Trees; Tables; Graphs; Recursion; Complexity analysis; Sorting and searching	Yr1 - Spring	CS	
92	2450	Intro to Software Engineering	The following topics will be covered in this course: Software life cycle models; Software project management; Team development environments and methodologies; Requirements engineering; Software design and architectures; Quality assurance and standards; Legal and ethical issues; Working as an effective member of a software development team	Yr2 - Spring	CS	
93	2700	Digital Circuits	This course is an introduction to digital systems, logic gates, combinational logic circuits, and sequential logic circuits.  It includes minimization techniques and implementation with encoders, decoders, multiplexers, and programmable logic devices.  It considers Mealy and Moore models of state machines, state minimization, and state assignment.  It also introduces a hardware description language.  This course is cross listed as ENGR 2700.	Yr1 - Spring	CS	MATH 1050
94	2810	Computer Organization and Architecture	The following topics will be covered in this course: Role of performance; Instruction sets and types; Assembly/machine language; Arithmetic; Datapath and control; Pipelining; Memory management; Interfacing and communication.	Yr2 - Fall	CS	
95	2830	Web Development III	The following topics will be covered in this course: - Introduction to Cloud Computing - Methods of Leveraging Cloud Computing - Cloud Economics and Total Cost of Ownership - AWS Infrastructure - AWS Compute, Storage, and Networking - AWS Security, Identity and Access Management - AWS Database Options - AWS Elasticity and Management Tools - Architecting on AWS and System Design - Methods of Designing Your Environment - System Design for High Availability - Automation and Serverless Architecture - Event-Driven Scaling	Yr4 - Fall	CS	
96	2860	Operating Systems	This course will cover the following topics: Operating systems history and architecture; Processes and Threads; Synchronization (mostly deadlock prevention); Memory Management; Input/Output; File Systems; Multimedia	Yr2 - Spring	CS	
97	1010	Expository Composition	This course emphasizes critical reading, writing, and thinking skills through writing-intensive workshops. It explores writing situations as a complex process focusing specifically on idea generation relative to audience and purpose, working through multiple drafts, peer collaboration, and revision, and it includes rhetorical analysis. See prerequisites.	Yr1 - Fall	ENGL	
98	2010	Intermediate Research Writing	Students will build on the skills learned in ENGL 1010 in this intermediate writing course designed to improve students reading, writing, research, and critical thinking skills. The course may include expository, persuasive, and/or argumentative writing emphases. The course will require several research oriented writing assignments. Students must achieve a C- or higher in this course to receive GE credit.	Yr1 - Spring	ENGL	ENGL 1010
99	3260	Technical Communications	This course focuses on professional, scientific, governmental, and technical discourse, including memos, letters, process descriptions, instructions, reports, and others in both print and digital media. Students will develop skills in audience awareness and rhetorical analysis, clarity and precision of expression, and document/visual design.	Yr3 - Spring	ENGL	ENGL 2010
100	1	Life Science			GE	
101	2	American Institutions		Yr1 - Spring	GE	
102	3	FA/HU/SS		Yr2 - Fall	GE	
103	4	FA/HU/SS		Yr2 - Spring	GE	
104	5	FA/HU/SS		Yr3 - Spring	GE	
105	1	Elective		Yr2 - Spring	MAT/SCI	
106	1210	Calculus I		Yr1 - Fall	MATH	MATH 1050, MATH 1060, MATH 1080
107	1220	Calculus II	This course is a continuation of the study of calculus. Topics include techniques of integration and applications, numeric integration techniques, calculus in conic sections and polar coordinates, infinite sequences and series (tests for convergence), and introduction to vectors.	Yr1 - Spring	MATH	MATH 1210
108	2270	Linear Algebra	Linear algebra is a study of systems of linear equations, matrices, vectors and vector spaces, linear transformations, eigenvalues and eigenvectors, and inner product spaces. This class is required for students majoring in mathematics and many areas of science and engineering.	Yr2 - Fall	MATH	MATH 1210
109	3040	Stats for Science & Engineers	This is a first course in statistics for STEM majors. Topics will include probability, discrete and continuous distributions, descriptive statistics, and statistical inference (confidence intervals and hypothesis testing, including linear regression and one-way ANOVA). Proficiency with integral calculus is required.	Yr3 - Fall	MATH	MATH 1210
110	3310	Discrete Mathematics	This course in discrete mathematics covers Boolean algebra, sets and relations, functions, induction, recursion, enumerative combinatorics, elements of number theory, complexity of algorithms, trees, and graph theory. 	Yr2 - Fall	MATH	MATH 1210
111	2210	Physics for Science & Engineers I	PHYS 2210 is the first semester of a two-semester sequence in calculus-based physics for scientists and engineers. A It is a necessary preparation for continuing studies in upper division STEM courses. A It includes an introduction to Newtons laws of motion, momentum and energy conservation, rotations, oscillations, waves, and gravitation. A The methods of calculus are applied to develop theories and to solve problems.	Yr2 - Fall	PHYS	MATH 1220
112	2215	Physics for Scientists and Engineers I Laboratory	PHYS 2215 is the laboratory experience to accompany PHYS 2210. Students will learn techniques of measurement and data analysis and to communicate scientific results effectively in writing. Principles from the lecture section will be illustrated.	Yr2 - Fall	PHYS	MATH 1220
113	2220	Physics for Science & Engineers II	 PHYS 2220 is the second semester of a two-semester sequence in calculus-based physics for scientists and engineers. It is a necessary preparation for continuing studies in upper division courses. It includes an introduction to electricity, magnetism, circuits, optics, and relativity. The methods of calculus are applied to develop theories and to solve problems.	Yr2 - Spring	PHYS	PHYS 2210
114	2225	Physics for Scientists and Engineers II Laboratory	PHYS 2225 is the laboratory experience to accompany PHYS 2220. Students will learn techniques of measurement and data analysis and to communicate scientific results effectively in writing. Students will get hands-on experience with the concepts taught in the lecture section.	Yr2 - Spring	PHYS	PHYS 2210
115	3250	Survey of Languages	This course will be divided into modules each covering a family of programming languages. In each module, the students will be introduced to a family of languages. We will discuss the use cases and thought patterns that the languages address. Additionally, for each module the students will be expected to learn to develop and debug in a representative language from the family being studied and complete a project using it. This course will cover the following modules: Functional Programming Haskell Interpreted Languages / Scripting Python Declarative Languages Prolog Big data Hadoop/Pig Latin Microsoft .Net C# In addition to the above modules the students will be expected to complete a final project and presentation which will consist of learning a language of their choice, completing a project in that language, and giving a short presentation to the class.	Yr3 - Fall	SE	
116	3410	Human Factors in Software Design	SE 3410 introduces the principles of user interface development, focusing on the following areas: • Design: We will look at how to design good user interfaces, covering important design principles (learnability, visibility, error prevention, efficiency, and graphic design) and the human capabilities that motivate them (including perception, motor skills, color vision, attention, and human error). • Implementation: We will see techniques for building user interfaces, including lowfidelity prototypes, Wizard of Oz, and other prototyping tools; input models, output models, model-view-controller, layout, constraints, and toolkits. • Evaluation: We will learn techniques for evaluating and measuring interface usability, including heuristic evaluation, predictive evaluation, and user testing. • Research: We will learn how to conduct empirical research involving novel user interfaces.	Yr3 - Fall	SE	
117	3450	Principles and Patterns of Software Design	The discipline of Software Engineering Design will be presented by covering the following topics: • Software Design processes • Software product design • Product design analysis • Software Engineering Design • Engineering Design • Architectural Design • Object Oriented Design • State Based Design • Patterns in Software Design	Yr3 - Spring	SE	
118	3520	Database Systems	The following topics will be covered in this course: • Foundations • Design Theory and Constraints • Transactions • Introduction to Database Systems • Specialized and New Data Processing Systems	Yr3 - Fall	SE	
119	3620	Distributed Application Development	Learn and experiment with Amazon Web Services (AWS): • console management • host a static website • build a web application • deploy a web application Understand distributed system components: • distributed system characteristics • system models • networking / internetworking • IPC / RPC • distributed objects • web services • peer to peer • security • distributed file systems • name services • time • transactions and concurrency control • replication	Yr3 - Spring	SE	
120	3630	Mobile Application Development	The following topics will be covered in this course: • Android App Development Tools • Creating and Debugging User Interfaces • User Interace Layouts • Activities and Navigation • Listviews • Input Controls • Multimedia: Android Media Player / Movies and Music • Geolocation: Mapping and location-based services • App Publishing / Content providers • Testing Strategies for Mobile Applications	Yr3 - Spring	SE	
121	4120	Management of Software Projects	This course covers the following topics: * Life cycle models * Requirements elicitation * Configuration control * Environments * Quality assurance	Yr4 - Fall	SE	
122	4140	Social and Ethical Issues in Computing	This course will cover the following modules:\n- History of the rapid rise in computer and networking technology.\n- Ethics including an introduction to the philosophical bases.\n- Networking and the Internet including Freedom of expression, Censorship, Spam, and\ninternet addiction.\n- Intellectual Property including copyright, fair use, patents, open-source, and licencing.\n- Privacy including the ownership of information, government surveillance, data mining\nand identify theft.\n- Computer and Network Security including Viruses, Hackers, and Denial-of-service\nattacks.\n- Computer Reliability including the cost and responsibility of errors, software\nwarranties, and simulations.\n- Professional Ethics including ACM and IEEE codes of conduct.	Yr4 - Spring	SE	
123	4220	Graphical User Interfaces	This course will cover the following modules:\nGUI Design and usability: Basic UI/UX considerations including: Discoverability, Error prevention, visibility, efficiency, perception, and navigation.\nInteraction between front and backend code: Common methods allowing interaction between GUI elements and the backend code of an application. These will include events, bindings, and MVC patterns.\nLayout Tools: Tools and patterns for laying out UI elements on the screen, including grids, canvas, panels, and wrapper elements.\nInput: Buttons, forms, mouse events, keyboard events, drag and drop events, and use of the clipboard.\nStyles and Themes: Reusable components for adjusting the visual aspects of the interface.\nResources and localization: Methods for creating international applications including localization (currencies, date formats, etc.) and languages.	Yr4 - Fall	SE	
124	4230	Advanced Algorithms	Algorithmic techniques will include divide-and-conquer, dynamic programming, greedy approaches, and graph algorithms as well as a selection of current algorithm topics of interest (e.g. learning algorithms). Analysis techniques will include the Master theorem, big-O/theta notation, and proofs by construction, contradiction and induction. Students will also practice implementing algorithms.	Yr4 - Spring	SE	
125	4320	Personal Software Practices	The course will cover the following topics:\n- The value of following defined processes.\n- Data collection and analysis for continuous improvement of the software development\nlife cycle.\n- Estimation and planning techniques of software projects.\n- Quality management and design principles.	Yr4 - Fall	SE	
126	4340	Secure Coding Practices	This course will cover the following modules:\n• Web Application Security & Practices including SQL injection, cross-site scripting, cross-site request forgery, cookies and hidden form fields.\n• Implementation Security & Practices including buffer overruns, string formatting issues, integer overflows, exceptions, command injection, information leakage, race conditions, principle of least privilege.\n• Cryptographic Security & Practices including weak passwords, weak cryptography, incorrect cryptography.\n• Networking Security & Practices including network security overview, secure network transmission, name resolution.\n• Vulnerability & risk mitigation, vulnerability assessments, & QA testing.	Yr4 - Spring	SE	
127	4400	Software Engineering Practicum I	This course will consist of a practical software engineering capstone experience. Content covered through the experience will be dependent upon the project assigned to each student. It is expected that each experience will be unique as provided for by a professional sponsor or assigned by the instructor. However, key principles taught throughout the Software Engineering curriculum will be applied to each setting.	Yr4 - Fall	SE	
128	4450	Software Engineering Practicum II	This course will consist of a practical software engineering capstone experience. Content covered through the experience will be dependent upon the project assigned to each student. It is expected that each experience will be unique as provided for by a professional sponsor or assigned by the instructor. However, key principles taught throughout the Software Engineering curriculum will be applied to each setting.	Yr4 - Spring	SE	
\.


--
-- Data for Name: coursePreReq; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."coursePreReq" (course_id, prereq_id) FROM stdin;
87	85
89	87
90	89
91	87
92	91
94	91
95	90
96	94
115	91
116	92
117	92
118	91
119	118
120	91
121	116
122	116
123	115
124	91
125	117
126	119
127	117
128	127
119	96
123	92
127	119
\.


--
-- Data for Name: course_learningoutcome; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.course_learningoutcome (course_id, learningoutcome_id) FROM stdin;
85	1
85	2
85	3
87	4
87	5
89	6
89	7
89	8
89	9
89	10
89	11
89	12
89	13
89	14
89	15
89	16
89	17
90	18
90	19
90	20
90	21
90	22
90	23
90	24
90	25
91	26
91	27
91	28
91	29
92	30
92	31
92	32
92	33
92	34
92	35
92	36
92	37
93	38
93	39
93	40
94	41
94	42
95	43
95	44
95	45
95	46
95	47
96	48
96	49
96	50
96	51
96	52
96	53
96	54
96	55
115	56
115	57
115	58
116	59
116	60
116	61
116	62
116	63
116	64
116	65
116	66
116	67
116	68
117	69
117	70
117	71
118	72
118	73
118	74
118	75
118	76
118	77
118	78
118	79
119	80
119	81
119	82
119	83
119	84
120	85
120	86
121	87
121	88
121	89
121	90
121	91
121	92
122	93
122	94
122	95
122	96
122	97
122	98
123	99
123	100
123	101
124	102
124	103
124	104
124	105
125	106
125	107
125	108
125	109
126	110
126	111
126	112
126	113
126	114
127	115
127	116
127	117
\.


--
-- Data for Name: learningoutcome; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.learningoutcome (id, name, description) FROM stdin;
1	\N	Students will know the basic data types, control structures, and programming approaches for a current programming language.  
2	\N	Students will be able to solve problems by developing algorithms and implementing those algorithms using a current programming language.
3	\N	Students will begin to understand the social responsibilities of the computing professional and the impact of computing on society
4	\N	Students will know some basic data structures, basic software methodologies, and machine level representation of data.
5	\N	Students will be able to apply appropriate software design methodologies for larger programs, use appropriate data structures, and use an object-oriented language.
6	\N	Students will be able to build a simple web site that organizes information effectively
7	\N	Students will be able to identify an organization for information based on its inherent structure (chronological, alphabetic, etc.).
8	\N	Students will be able to use cascading style sheets to create style standards for a web site.
9	\N	Students will be able to create a navigational framework that matches the content and genre of the site.
10	\N	Students will be able to explain separation of concerns as it applies to the design and implementation of a web site
11	\N	Students will be able to describe the issues involved in developing a web interface.
12	\N	Students will be able to summarize the need and issues involved in web site implementation and integration.
13	\N	Students will be able to explain why accessibility issues are an important consideration in web page development.
14	\N	Students will be able to design and implement a web interface
15	\N	Students will be able to compare/contrast graphic media file format characteristics such as color depth, compression and CODEC
16	\N	Students will be able to explain and compare media file formats including lossy vs. lossless compression, color palettes, streaming formats, and CODECs
17	\N	Students will be able to explain and compare the inter-operability of formats
18	\N	Demonstrate how server-side technology works.
19	\N	Develop with server-side technology. Complicated web development environments such as ASP.NET and PHP have a fairly substantial learning curve.
20	\N	Utilize databases in web applications.
21	\N	Implement common data models used in blogs, forums, and content management systems.
22	\N	Design software for web applications. This includes layered software architectures as well as tiered designs for scalability and reliability.
23	\N	Identify mechanisms for maintaining state in web applications. This is one of the most important topics in the course since it is the principal difference between web application development and non-web application development.
24	\N	Consume REST and SOAP web services.
25	\N	Design and implementing web security.
26	\N	Students will know many basic data structures, recursion, complexity analysis, and common sorting and searching algorithms.
27	\N	Students will be able to use appropriate data structures and data abstraction. They will be able to use recursion as a problem solving strategy. They will be able to analyze complexity of algorithms and use appropriate sorting and searching algorithms.
28	\N	Students will know many basic data structures, recursion, complexity analysis, and common sorting and searching algorithms.
29	\N	Students will be able to use appropriate data structures and data abstraction. They will be able to use recursion as a problem solving strategy. They will be able to analyze complexity of algorithms and use appropriate sorting and searching algorithms.
30	\N	Underststand software engineering principles.
31	\N	Specify and manage software project requirements.
32	\N	Perform basic software project management.
33	\N	Understand the impact of alternative software development practices like extreme programming (XP) and Agile.
34	\N	Perform unit testing of software.
35	\N	Create work product documentation.
36	\N	Perform project risk management.
37	\N	Work as an effective member of a software development team.
38	\N	Students will understand and be able to work with the number systems around which digital computer hardware is designed, including conversion between number systems, two complement binary numbers, and binary arithmetic.
39	\N	Students will be able to use Boolean algebra as a tool in the design of digital circuits which are to perform a given logic function, and to use Kamaugh maps to find the minimal realization of such circuits.  
40	\N	Students will have a basic understanding of digital logic to prepare him or her to transfer into the professional engineering program at a university, and there continue the study of digital logic at an advanced level.
41	\N	Students will understand characteristics of an instruction set architecture, an assembly language, assembly level machine organization, and performance and compilation issues.
42	\N	Students will be able to analyze computer system organization at the assembly language level, implement algorithms in assembly/machine language, and design a computer system at a block level.
43	\N	Students will be able to employ the public infrastructure as a Service (IaaS) cloud known as Amazon Web Services (AWS) to deploy an existing computer application using cloud computing technologies such as load balancing and auto-scaling.
44	\N	Students will be able to, as part of application deployment, use security controls and technologies available in AWS such as identity and access management, virtual private clouds, security groups, etc.
45	\N	Students will be able to appraise the usefulness of the above-mentioned controls and technologies in terms of relevan security principles and / or attach methods.
46	\N	Students will be able to understand the considerations of architecting a cloud solution.
47	\N	Students will be able to compare and contrast public IaaS with other cloud technologies and models, particularly with respect to resource requirements and security issues.
48	\N	Mechanisms and policies that enable the efficient illusion of concurrent execution of multiple programs upon one (or more) CPUs.
49	\N	Mechanisms and policies that enable a "virtual" address spaces that may be larger than allocated memory.
50	\N	Security mechanisms and policies that protect processes from unauthorized access to their memory or files.
51	\N	Data structures and algorithms used to organize persistent storage.
52	\N	How resources such as CPU time, memory, and persistent storage are managed and allocated.
53	\N	Challenges in concurrency and approaches to avoiding race conditions and deadlock.
54	\N	The intertwined role of interrupts, traps, paged and segmented memory translation, and supervisor mode in enabling multi-tasking, isolation, system calls, access to i/o devices, and timers.
55	\N	Coarse estimations of access time to persistent storage.
56	\N	Understand the tradeoffs between, use cases for, and theory behind different types of programming languages.
57	\N	Feel comfortable with their ability to learn and use unfamiliar programming languages.
58	\N	Be exposed to and implement programs in a wide variety of programming languages that are used in the software industry.
59	\N	Gather useful information about users and activities through asking, looking, learning, and experimenting with alternatives.
60	\N	Organize information about users into useful summaries with affinity diagrams.
61	\N	Convey user research findings with personas and scenarios.
62	\N	Learn and appreciate the skill of sketching as a process for user experience design.
63	\N	Learn to give and accept critiques of design ideas in a constructive manner.
64	\N	Demonstrate skills for low-fidelity prototyping and describe the strengths and weaknesses of a variety of prototyping methods.
65	\N	Appreciate the process of user experience design as a cyclical, iterative process.
66	\N	Understand the differences between usability and user experience.
67	\N	Analyze an interaction design problem and propose a user-centered process, justifying the process and identifying the trade-offs.
68	\N	Prepare high quality professional documentation and artifacts relating to the design process for preparation for a professional portfolio.
69	\N	Demonstrated use of design patterns to reinforce design principles.
70	\N	Developed vocabulary of object-oriented design patterns and principles.
71	\N	Awareness of increase ability to create quality software.
72	\N	Students will learn the techniques and tools to design, guild, and extract information from a database.
73	\N	Understand database design and the use of databases in applications.
74	\N	Understand the relational model, relational algebra, and SQL.
75	\N	Utilize database design and relational design principles based on dependencies and normal forms.
76	\N	Analyze complex business scenarios and create data models
77	\N	Implement their database design by creating a physical database using SQL
78	\N	Successfully implement indexes, views, transaction and integrity constraints.
79	\N	Successfully deliver a project which explores database design and management in web applications by utilizing appropriate features of SQL.
80	\N	Students will demonstrate understanding of distributed operating system and network protocols for process communication, synchronization, scheduling, exception and deadlock resolution.
81	\N	Students will demonstrate via use their understanding of client-server, web-based collaborative systems.
82	\N	Students will demonstrate understanding of parallel computing.
83	\N	Students will demonstrate their ability to solve concurrency issues.
84	\N	Students will exercise APIs for distributed application development.
85	\N	Students will become familiar with a mobile applications development environment and life cycle. They will know the basics of the Android environment, tools for creating Android applications, the Android approach to structuring applications, basic user interfaces, and the application life cycle.
86	\N	Students will be able to design, implement, test, and debug mobile applications.
87	\N	Students will be able to write a software project management plan, addressing issues of risk analysis, schedule, costs, team organization, resources, and technical approach.
88	\N	Students will be able to define the technology and practices associated with each and a variety of software development life cycle models and explain the strengths, weaknesses, and applicability of each.
89	\N	Students will be able to define the relationship between software products and overall products (if embedded), or the role of the product in the organizational product line.
90	\N	Students will be able to explain the purpose and limitations of software development standards and be able to apply sensible tailoring where needed.
91	\N	Students will be able to utilize software development standards for documentation and implementation.
92	\N	Students will be able to apply leadership principles to project management.
93	\N	Students will understand the connections and interactions between science, technology and society.
94	\N	Students will understand current Intellectual Property debates and the relevant laws governing them.
95	\N	Students will understand privacy concerns posed by modern information gathering methods.
96	\N	Students will be exposed to industry standard codes of ethics put out by ACM and IEEE.
97	\N	Students will understand the history of computing and their place in it.
98	\N	Students will be exposed to philosophical bases for evaluating computer ethics.
99	\N	Students will be able to recognize and use common GUI framework elements, patterns, and concepts.
100	\N	Students will know a modern GUI framework and be comfortable working within it.
101	\N	Students will be familiar with testing and validation strategies for UI frameworks including unit testing, integration testing, and end to end testing.
102	\N	Students will be able to design algorithms to solve a variety of computational problems. They will be able to employ any of the following techniques: divide-and-conquer, dynamic programming, greedy approaches, and graph algorithms.
103	\N	Students will be able to analytically reason about and prove the correctness/incorrectness and complexity of algorithmic solutions to problems.
104	\N	Students will be able to implement algorithms in software.
105	\N	Students will be able to reason about the computational complexity of a computation problem by reduction to other problems.
106	\N	Students will demonstrate an ability to reduce overall software development defect rates.
107	\N	Students will demonstrate the importance of the time spent at the front end of the development cycle to lay the foundation for a successful project.
108	\N	Students will demonstrate an ability to eliminate or nearly eliminate compile and test defects.
109	\N	Students will be able to accurately estimate the time requirements to build software.
110	\N	Students will be able to explain security design principles.
111	\N	Students will be able to apply security principles when they analyze and design projects.
112	\N	Students will be able to implement projects using security primitives.
113	\N	Students will be able to utilize tools for security analysis.
114	\N	Students will be able to evaluate the security of project implementations.
115	\N	Students will be able to apply a systematic approach to common engineering problems in the context of their practicum project.
116	\N	Students will demonstrate an ability to apply theories and principles of Software Engineering to problems encountered in industry.
117	\N	Students will demonstrate an ability to exercise and improve time and stress management skills as well as problem-solving skills
\.


--
-- Data for Name: learningoutcome_post; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.learningoutcome_post (here, post) FROM stdin;
\.


--
-- Data for Name: learningoutcome_pre; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.learningoutcome_pre (here, pre) FROM stdin;
\.


--
-- Data for Name: skill; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.skill (id, name, learningoutcome_id) FROM stdin;
\.


--
-- Name: course_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.course_id_seq', 128, true);


--
-- Name: learningobjective_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.learningobjective_id_seq', 117, true);


--
-- Name: coursePreReq coursePreReq_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."coursePreReq"
    ADD CONSTRAINT "coursePreReq_pkey" PRIMARY KEY (prereq_id, course_id);


--
-- Name: course_learningoutcome course_learningoutcome_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.course_learningoutcome
    ADD CONSTRAINT course_learningoutcome_pkey PRIMARY KEY (course_id, learningoutcome_id);


--
-- Name: course course_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.course
    ADD CONSTRAINT course_pkey PRIMARY KEY (id);


--
-- Name: learningoutcome learningobjective_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.learningoutcome
    ADD CONSTRAINT learningobjective_pkey PRIMARY KEY (id);


--
-- Name: learningoutcome_post learningoutcome_post_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.learningoutcome_post
    ADD CONSTRAINT learningoutcome_post_pkey PRIMARY KEY (here, post);


--
-- Name: learningoutcome_pre learningoutcome_pre_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.learningoutcome_pre
    ADD CONSTRAINT learningoutcome_pre_pkey PRIMARY KEY (here, pre);


--
-- Name: skill skill_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.skill
    ADD CONSTRAINT skill_pkey PRIMARY KEY (id);


--
-- Name: coursePreReq courseFK; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."coursePreReq"
    ADD CONSTRAINT "courseFK" FOREIGN KEY (course_id) REFERENCES public.course(id) NOT VALID;


--
-- Name: course_learningoutcome course_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.course_learningoutcome
    ADD CONSTRAINT course_id FOREIGN KEY (course_id) REFERENCES public.course(id) NOT VALID;


--
-- Name: learningoutcome_pre herefk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.learningoutcome_pre
    ADD CONSTRAINT herefk FOREIGN KEY (here) REFERENCES public.learningoutcome(id);


--
-- Name: learningoutcome_post herefk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.learningoutcome_post
    ADD CONSTRAINT herefk FOREIGN KEY (here) REFERENCES public.learningoutcome(id);


--
-- Name: course_learningoutcome learningobj_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.course_learningoutcome
    ADD CONSTRAINT learningobj_id FOREIGN KEY (learningoutcome_id) REFERENCES public.learningoutcome(id) NOT VALID;


--
-- Name: skill learningoutcome_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.skill
    ADD CONSTRAINT learningoutcome_fk FOREIGN KEY (learningoutcome_id) REFERENCES public.learningoutcome(id) NOT VALID;


--
-- Name: learningoutcome_post postfk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.learningoutcome_post
    ADD CONSTRAINT postfk FOREIGN KEY (post) REFERENCES public.learningoutcome(id);


--
-- Name: learningoutcome_pre prefk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.learningoutcome_pre
    ADD CONSTRAINT prefk FOREIGN KEY (pre) REFERENCES public.learningoutcome(id);


--
-- Name: coursePreReq prereqFK; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."coursePreReq"
    ADD CONSTRAINT "prereqFK" FOREIGN KEY (prereq_id) REFERENCES public.course(id) NOT VALID;


--
-- PostgreSQL database dump complete
--

