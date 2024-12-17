// DOMContentLoaded Event Listener for initializing chart and calendar
document.addEventListener("DOMContentLoaded", () => {
   // initializeChart();
    generateCalendar();
});

// Function to initialize the chart
/*function initializeChart() {
    const ctx = document.getElementById("coverageChart").getContext("2d");

    const data = {
        labels: ["Completed", "Overdue", "Due Soon", "Scheduled"],
        datasets: [{
            data: [25, 50, 15, 10], // Example percentages, replace with dynamic data if needed
            backgroundColor: ["#22c55e", "#dc2626", "#eab308", "#3b82f6"],
            hoverBackgroundColor: ["#16a34a", "#b91c1c", "#ca8a04", "#2563eb"],
            borderColor: "#ffffff",
            borderWidth: 2,
        }],
    };

    const config = {
        type: "pie",
        data: data,
        options: {
            responsive: true,
            plugins: {
                legend: { position: "bottom" },
                tooltip: {
                    callbacks: {
                        label: (context) => `${context.label}: ${context.raw}%`,
                    },
                },
            },
        },
    };

    new Chart(ctx, config);
}*/

// Function to generate the calendar
function generateCalendar(year, month) {
    const calendarGrid = document.getElementById("calendarGrid");
    calendarGrid.innerHTML = ""; // Clear existing grid
    const currentMonth = document.getElementById("currentMonth");

    // Update the month title
    currentMonth.textContent = new Date(year, month).toLocaleString("default", { month: "long", year: "numeric" });

    // Get the first day and total days of the month
    const firstDay = new Date(year, month, 1).getDay();
    const daysInMonth = new Date(year, month + 1, 0).getDate();

    // Generate blank spaces for days before the first day
    for (let i = 0; i < firstDay; i++) {
        const blank = document.createElement("div");
        blank.classList.add("calendar-cell");
        calendarGrid.appendChild(blank);
    }

    // Generate days with dots for meetings
    for (let day = 1; day <= daysInMonth; day++) {
        const cell = document.createElement("div");
        cell.classList.add("calendar-cell");
        cell.textContent = day;

        // Check if the current day has meetings
        const dateStr = `${year}-${String(month + 1).padStart(2, "0")}-${String(day).padStart(2, "0")}`;
        const meetingData = meetings.find((m) => m.date === dateStr);

        if (meetingData) {
            const dotContainer = document.createElement("div");
            dotContainer.classList.add("dot-container");

            // Add a dot for each meeting
            meetingData.meetings.forEach((meeting, index) => {
                const dot = document.createElement("span");

                // Assign colors dynamically
                if (index % 3 === 0) {
                    dot.classList.add("dot", "red"); // Red dot
                } else if (index % 3 === 1) {
                    dot.classList.add("dot", "blue"); // Blue dot
                } else {
                    dot.classList.add("dot", "green"); // Green dot
                }

                dotContainer.appendChild(dot);
            });

            cell.appendChild(dotContainer);
        }

        calendarGrid.appendChild(cell);
    }
}

// Initialize calendar
const today = new Date();
let currentYear = today.getFullYear();
let currentMonth = today.getMonth();

function previousMonth() {
    currentMonth--;
    if (currentMonth < 0) {
        currentMonth = 11;
        currentYear--;
    }
    generateCalendar(currentYear, currentMonth);
}

function nextMonth() {
    currentMonth++;
    if (currentMonth > 11) {
        currentMonth = 0;
        currentYear++;
    }
    generateCalendar(currentYear, currentMonth);
}

// Sample meeting data
const meetings = [
    { date: "2024-11-10", meetings: ["Meeting 1", "Meeting 2"] },
    { date: "2024-11-15", meetings: ["Meeting 3", "Meeting 4"] },
    { date: "2024-11-20", meetings: ["Meeting 5", "Meeting 6", "Meeting 7"] }
];

// Render the initial calendar
generateCalendar(today.getFullYear(), today.getMonth());

// Employee Card JS

// Display the Notes Popup
function showNotesPopup() {
    document.getElementById('notesPopup').style.display = 'block';
}

// Display the Employee Details Popup
function showEmployeeDetails() {
    document.getElementById('employeeDetailsPopup').style.display = 'block';
}

// Close a Popup
function closePopup(popupId) {
    document.getElementById(popupId).style.display = 'none';
}

// Save Meeting Notes
function saveNotes() {
    const notes = document.getElementById('meetingNotes').innerHTML.trim();
    const savedNotesDiv = document.getElementById('savedNotes');
    const rating = document.getElementById('rating').value || 'Not Rated';
    const image = document.querySelector('#imagePreview img')?.src || 'No image attached';

    if (notes) {
        savedNotesDiv.innerHTML = `
            <p>${notes}</p>
            <p><strong>Rating:</strong> ${rating}</p>
            <div><strong>Attached Image:</strong><br>
                ${image !== 'No image attached' ? `<img src="${image}" style="max-width: 100px; border-radius: 5px;">` : 'No image attached'}
            </div>`;
        alert('Notes saved successfully!');
    } else {
        alert('Please enter some notes.');
    }

    closePopup('notesPopup');
}

// Apply Text Formatting (Bold, Italic, Underline)
function formatText(command) {
    if (['bold', 'italic', 'underline'].includes(command)) {
        document.execCommand(command, false, null);
    }
}

// Add Unordered List (Bullets) with a Specific Style
function addList(styleType) {
    document.execCommand('insertUnorderedList', false, null);
    applyListStyle('UL', styleType);
}

// Function to add an ordered list (numbering) with a specific style
function addNumberedList(styleType) {
    document.execCommand('insertOrderedList', false, null);
    applyListStyle('OL', styleType);
}

// Function to apply specific styles to lists (UL or OL)
function applyListStyle(listType, styleType) {
    const lists = document.getElementsByTagName(listType);
    for (let i = 0; i < lists.length; i++) {
        const list = lists[i];
        switch (styleType) {
            case 'classic':
                list.style.listStyleType = 'disc';
                break;
            case 'square':
                list.style.listStyleType = 'square';
                break;
            case 'circle':
                list.style.listStyleType = 'circle';
                break;
            case 'decimal':
                list.style.listStyleType = 'decimal';
                break;
            case 'lower-alpha':
                list.style.listStyleType = 'lower-alpha';
                break;
            case 'upper-roman':
                list.style.listStyleType = 'upper-roman';
                break;
            default:
                list.style.listStyleType = 'disc';
                break;
        }
        list.style.border = '2px solid #ccc';
        list.style.padding = '10px';
    }
}

// Function to apply styles to list items (like adding a border to each list item)
function applyListItemStyles() {
    const listItems = document.querySelectorAll('ul li, ol li');
    listItems.forEach(item => {
        item.style.padding = '5px 10px';
        item.style.borderBottom = '1px solid #ddd';
    });
}

// Add a listener to handle when lists are created and styles need to be applied
document.addEventListener('DOMSubtreeModified', function () {
    applyListItemStyles();
});

// Attach Image Function
function attachImage() {
    document.getElementById('imageInput').click();
}

// Preview Attached Image
function previewImage(event) {
    const file = event.target.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = function (e) {
            const preview = document.getElementById('imagePreview');
            preview.innerHTML = `<img src="${e.target.result}" alt="Attached Image" style="max-width: 100%; border-radius: 8px; box-shadow: 0 2px 6px rgba(0,0,0,0.2);">`;
        };
        reader.readAsDataURL(file);
    }
}

// Schedule Discussion Option - Start/End Time Logic
document.getElementById('start-time').addEventListener('input', function () {
    let startTime = document.getElementById('start-time').value;
    let [startHour, startMinute] = startTime.split(':').map(num => parseInt(num));

    let endHour = startHour;
    let endMinute = startMinute + 30;

    if (endMinute >= 60) {
        endMinute -= 60;
        endHour += 1;
    }

    if (endHour >= 24) {
        endHour = 23;
        endMinute = 59;
    }

    document.getElementById('end-time').value = `${String(endHour).padStart(2, '0')}:${String(endMinute).padStart(2, '0')}`;
});


//HR-dashboard

/*document.addEventListener("DOMContentLoaded", () => {
    const unitData = [
        { unit: "Unit A", departments: [{ name: "HR", eligible: 20, completed: 15 }, { name: "Finance", eligible: 10, completed: 8 }] },
        { unit: "Unit B", departments: [{ name: "IT", eligible: 15, completed: 12 }, { name: "Admin", eligible: 8, completed: 5 }] }
    ];

    const roleAccess = ["BU-HR Head", "Coordinator"];
    const currentUserRole = "BU-HR Head";

    console.log("Unit data:", unitData);
    console.log("Current user role:", currentUserRole);

    const chartCanvas = document.getElementById("eligibleVsCompletedChart");
    const unitList = document.getElementById("unitList");
    const detailsSection = document.getElementById("details");
    const detailsTable = document.getElementById("detailsTable");
    const backToUnitsButton = document.getElementById("backToUnits");

    if (!roleAccess.includes(currentUserRole)) {
        alert("You do not have access to this dashboard.");
        return;
    }

    // Populate Units
    unitData.forEach((unit, index) => {
        const div = document.createElement("div");
        div.className = "unit";
        div.textContent = unit.unit;
        div.addEventListener("click", () => showUnitDetails(index));
        unitList.appendChild(div);
    });

    console.log("Units populated successfully.");*/

    // Initialize Chart
    if (chartCanvas) {
        new Chart(chartCanvas, {
            type: "bar",
            data: {
                labels: ["Eligible", "Completed"],
                datasets: [
                    {
                        label: "Meetings",
                        data: [35, 28],
                        backgroundColor: ["#4CAF50", "#FF9800"]
                    }
                ]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: {
                        display: true
                    }
                }
            }
        });
        console.log("Chart initialized.");
    } else {
        console.error("Chart canvas not found.");
    }

    // Show Unit Details
    function showUnitDetails(index) {
        const selectedUnit = unitData[index];
        unitList.classList.add("hidden");
        detailsSection.classList.remove("hidden");

        detailsTable.innerHTML = "";
        selectedUnit.departments.forEach(department => {
            const row = document.createElement("tr");
            row.innerHTML = `
                <td>${department.name}</td>
                <td>${department.eligible}</td>
                <td>${department.completed}</td>
            `;
            detailsTable.appendChild(row);
        });
        console.log("Details for unit:", selectedUnit.unit);
    }

    // Back to Units
    backToUnitsButton.addEventListener("click", () => {
        detailsSection.classList.add("hidden");
        unitList.classList.remove("hidden");
        console.log("Returning to unit list.");
    });
});
