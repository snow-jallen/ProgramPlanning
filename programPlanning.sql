--
-- PostgreSQL database dump
--

-- Dumped from database version 12.0 (Debian 12.0-2.pgdg100+1)
-- Dumped by pg_dump version 12.0 (Debian 12.0-2.pgdg100+1)

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
\.


--
-- Data for Name: course_learningoutcome; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.course_learningoutcome (course_id, learningoutcome_id) FROM stdin;
\.


--
-- Data for Name: learningoutcome; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.learningoutcome (id, name, description) FROM stdin;
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

SELECT pg_catalog.setval('public.learningobjective_id_seq', 1, false);


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

