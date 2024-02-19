const uri = 'api/Problems';
let problems = [];

function getItems() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));

}


function addItem() {
    const addTitleTextbox = document.getElementById('add-title');
    const addProblemTextbox = document.getElementById('add-problem');
    const addSolutionTextbox = document.getElementById('add-solution');
    const addTypeTextbox = document.getElementById('add-type');

    const item = {
        ProblemName: addTitleTextbox.value.trim(),
        Problem: addProblemTextbox.value.trim(),
        Solution: addSolutionTextbox.value.trim(),
        Type: addTypeTextbox.value.trim()
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getItems();
            addTitleTextbox.value = '';
            addProblemTextbox.value = '';
            addSolutionTextbox.value = '';
            addTypeTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}






function displayEditForm(problemNamee) {
    const item = problems.find(item => item.problemName === problemNamee);

    document.getElementById('edit-title').value = item.problemName;
    document.getElementById('edit-problem').value = item.problem;
    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-solution').value = item.solution;
    document.getElementById('edit-type').value = item.type;
    document.getElementById('editForm').style.display = 'block';
}




function deleteItem(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}







function updateItem() {
    const itemId = document.getElementById('edit-id').value;
    const item = {

        ProblemName: document.getElementById('edit-title').value.trim(),
        Problem: document.getElementById('edit-problem').value.trim(),
        Solution: document.getElementById('edit-solution').value.trim(),
        Type: document.getElementById('edit-type').value.trim(),
    };



    fetch(`${uri}/${itemId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to update item.', error));




    closeInput();

    return false;
}




function closeInput() {
    document.getElementById('editForm').style.display = 'none';
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


        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm('${item.problemName}')`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem('${item.id}')`);

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



        let td5 = tr.insertCell(4);
        td5.appendChild(editButton);

        let td6 = tr.insertCell(5);
        td6.appendChild(deleteButton);
    });

    problems = data;
}