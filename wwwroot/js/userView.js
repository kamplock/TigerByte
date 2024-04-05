const uri = 'api/Problems';
let problems = [];

function getItems() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));

}

function filterProblems() {
    const filterType = document.getElementById('filterDropdown').value;
    const tBody = document.getElementById('problems');

    // Clear any previously highlighted rows
    clear();

    // Iterate over each problem row to check for type match
    problems.forEach((problem, index) => {
        if (problem.type.toLowerCase() === filterType.toLowerCase() || filterType === 'clear') {
            // Highlight the row by adding a CSS class
            tBody.rows[index].classList.add('highlight');
        }
    });
}

function clear() {
    const tBody = document.getElementById('problems');
    // Remove highlight from all rows
    for (let i = 0; i < tBody.rows.length; i++) {
        tBody.rows[i].classList.remove('highlight');
    }
}


function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'problem' : 'problems';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}







function _displayItems(data) {
    const tBody = document.getElementById('problems');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {


        

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNode1 = document.createTextNode(item.problemName);
        td1.appendChild(textNode1);

        let td2 = tr.insertCell(1);
        let textNode2 = document.createTextNode(item.problem);
        td2.appendChild(textNode2);

        let td3 = tr.insertCell(2);
        let textNode3 = document.createTextNode(item.solution);
        td3.appendChild(textNode3);

        let td4 = tr.insertCell(3);
        let textNode4 = document.createTextNode(item.type);
        td4.appendChild(textNode4);



    });

    problems = data;
}