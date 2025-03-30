DELETE FROM ToDoItem

INSERT INTO ToDoItem (Name, Description, Priority, CreatedAt, IsDone)
VALUES
-- ToDo item with description and high priority
('Finish Project Documentation', 'Complete the final documentation for the project, including API references, user guides, and installation instructions. Make sure to review the content for clarity and consistency.', 3, '2025-03-01 08:30:00', 0),

-- ToDo item with description and medium priority
('Prepare for Meeting', 'Review the agenda for the upcoming team meeting. Prepare slides for the presentation on the new project proposals, and make sure all participants are notified.', 2, '2025-03-02 10:00:00', 0),

-- ToDo item with description and low priority
('Organize Workspace', 'Take some time to clean and organize the desk. Sort out files and arrange the workspace for better productivity. This should be a quick task that will take less than 30 minutes.', 1, '2025-03-03 12:15:00', 1),

-- ToDo item without description and high priority
('Fix Critical Bug in Application', NULL, 3, '2025-03-04 14:45:00', 0),

-- ToDo item with description and medium priority
('Update Resume', 'Add recent work experience and skills to the resume. Ensure that the format is clean and professional. Tailor the resume for roles in the tech industry.', 2, '2025-03-05 09:30:00', 0),

-- ToDo item without description and low priority
('Read Book', NULL, 1, '2025-03-06 11:00:00', 0),

-- ToDo item with description and medium priority
('Schedule Doctor Appointment', 'Call the local health clinic to schedule a check-up appointment for next week. Make sure to ask for availability on Wednesday or Friday.', 2, '2025-03-07 13:00:00', 1),

-- ToDo item with description and low priority
('Clean Email Inbox', 'Go through the email inbox and delete unnecessary emails. Organize remaining ones into folders. Unsubscribe from irrelevant newsletters and email lists.', 1, '2025-03-08 15:30:00', 1),

-- ToDo item without description and high priority
('Attend Training Session', NULL, 3, '2025-03-09 16:00:00', 0),

-- ToDo item with description and low priority
('Buy Groceries', 'Pick up groceries from the store. Get vegetables, fruits, milk, and eggs. Don’t forget to check if there are any promotions on bread or cereal.', 1, '2025-03-10 17:45:00', 0),

-- ToDo item with description and medium priority
('Review Monthly Budget', 'Check the budget for the current month, and compare actual expenses to projected costs. Adjust any planned purchases if necessary to stay within budget.', 2, '2025-03-11 18:00:00', 0),

-- ToDo item with description and high priority
('Complete Tax Filing', 'Ensure all documents for tax filing are prepared. Double-check all deductions and credits, and file the return before the deadline. Consult with a tax professional if needed.', 3, '2025-03-12 08:00:00', 1),

-- ToDo item with description and medium priority
('Research New Technology Trends', 'Read articles, watch videos, and explore recent tech trends in AI and machine learning. Write a summary of key findings and their potential applications for the team.', 2, '2025-03-13 09:15:00', 0),

-- ToDo item with description and low priority
('Plan Weekend Trip', 'Look up travel destinations, accommodations, and activities for the weekend getaway. Prepare a small itinerary for the trip and book the accommodations in advance.', 1, '2025-03-14 10:30:00', 1),

-- ToDo item without description and low priority
('Exercise', NULL, 1, '2025-03-15 11:00:00', 0),

-- ToDo item with description and medium priority
('Prepare Client Presentation', 'Gather data and insights for the upcoming client presentation. Create slides with key talking points and ensure all necessary documents are ready for review.', 2, '2025-03-16 12:45:00', 0),

-- ToDo item with description and high priority
('Emergency Server Maintenance', 'Perform emergency maintenance on the server to address security vulnerabilities. Ensure no downtime, and verify system backups before starting the process.', 3, '2025-03-17 13:30:00', 1),

-- ToDo item without description and high priority
('Fix Server Issue', NULL, 3, '2025-03-18 14:00:00', 0),

-- ToDo item with description and low priority
('Learn New Programming Language', 'Explore resources on a new programming language that could help improve coding skills. Set aside time each day to study and practice basic examples.', 1, '2025-03-19 15:00:00', 1),

-- ToDo item with description and medium priority
('Plan Next Marketing Campaign', 'Discuss with the marketing team to brainstorm ideas for the next campaign. Create a timeline and decide on platforms for promoting the campaign.', 2, '2025-03-20 16:30:00', 0),

-- ToDo item without description and low priority
('Watch Movie', NULL, 1, '2025-03-21 17:15:00', 0),

-- ToDo item with long name
('Check the solution for the long names in the card..........', NULL, 3, '2025-03-28 14:07:00', 0);