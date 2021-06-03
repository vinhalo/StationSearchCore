function filterStations(e) {
    const url = "/Stations/GetStations?filter=";

    fetch(url + e + '"')
        .then(function (response) {
            return response.json();
        })
        .then(function (data) {
            Array.from(document.getElementsByClassName('btn')).forEach(function (btn) {
                enableStationButtons(btn, data.nextPossibleCharacters);
                enableBackspaceButton(data.stations);
                clearSearcResultItems();

                data.stations.forEach(function (s) {
                    addSearchResultItem(s);
                });
            });
        });
}

const enableStationButtons = (btn, nextPossibleCharacters) => {
    const found = nextPossibleCharacters.includes(btn.innerHTML);
    btn.disabled = found == false;
};

const enableBackspaceButton = (stations) => {
    const hasStations = stations.length > 0;
    const backspaceButton = document.querySelector('.backspaceBtn');
    backspaceButton.disabled = hasStations == false;
};

const clearSearcResultItems = () =>
    document.querySelector('#searchResults > tbody').innerHTML = '';

const addSearchResultItem = (s) => {
    let row = document.createElement('tr');
    let cell = document.createElement('td');
    cell.innerHTML = s; // still not happy with manipulating innerHTML
    row.appendChild(cell);
    document.querySelector('#searchResults > tbody').appendChild(row);
};

function changeStationNameFilterText(newfilter) {
    const stationName = document.getElementById('stationName');
    stationName.value = newfilter;
    stationName.dispatchEvent(new Event('change'));
}

(function () {
    const stationName = document.getElementById('stationName');
    stationName.addEventListener('change', function (e) {
        filterStations(this.value);
    });
    stationName.addEventListener('paste', function (e) {
        filterStations(this.value);
    });
    stationName.addEventListener('keyup', function (e) {
        filterStations(this.value);
    });

    Array.from(document.getElementsByClassName('btn')).forEach(function (el) {
        el.addEventListener('click', function () {
            var currentText = stationName.value;
            changeStationNameFilterText(currentText + this.textContent);
        });
    });

    document.querySelector('.backspaceBtn').addEventListener('click', function () {
        var currentText = stationName.value;
        changeStationNameFilterText(currentText.substring(0, currentText.length - 1));
    });

    changeStationNameFilterText("");
})();
