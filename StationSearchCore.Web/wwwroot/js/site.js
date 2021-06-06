import { makeAutoObservable, autorun } from 'https://cdn.skypack.dev/mobx';

function makeSearchStore() {
    // Application state
    const store = makeAutoObservable({
        stations: [],               // observable

        nextPossibleCharacters: [], // observable

        get hasStations() {         // computed. Changes in reaction to stations
            return this.stations.length > 0
        },

        get charactersEnabled() {   // computed. Changes in reaction to nextPossibleCharacters
            const alphabet = [...'ABCDEFGHIJKLMNOPRQRSTUVWXYZ '];
            return alphabet.map(x => [x, this.nextPossibleCharacters.includes(x)]);
        },

        filterStations(e) {         // action.
            const url = "/Stations/GetStations?filter=";

            fetch(`${url}${encodeURIComponent(e)}"`) // Why add d-quote here and remove in backend?
                .then(function (response) {
                    return response.json();
                })
                .then(function (data) {
                    store.stations.replace(data.stations);
                    store.nextPossibleCharacters.replace(data.nextPossibleCharacters);
                }).catch((error) => {
                    console.log(error); // network error here
                });
        }
    });

    return store;
}

const searchStore = makeSearchStore(); // create singleton store

const enableBackspaceButton = (hasStations) => {
    const backspaceButton = document.querySelector('.backspaceBtn');
    backspaceButton.disabled = hasStations == false;
};

const clearSearchResultItems = () =>
    document.querySelector('#searchResults > tbody').innerHTML = '';

const addSearchResultItem = (s) => {
    let row = document.createElement('tr');
    let cell = document.createElement('td');
    cell.innerHTML = s; // still not happy with manipulating innerHTML
    row.appendChild(cell);
    document.querySelector('#searchResults > tbody').appendChild(row);
};

const enableCharacters = ([alpha, enabled]) => {
    const a = alpha == " " ? "space" : alpha;
    const btn = document.querySelector(`[data-alpha="${a}"]`)
    btn.disabled = enabled == false;
}

// Hook up reaction to changes in searchStore observables
autorun(() => {

    enableBackspaceButton(searchStore.hasStations);

    searchStore.charactersEnabled.forEach(enableCharacters);

    clearSearchResultItems();
    searchStore.stations.forEach(addSearchResultItem);
});

function changeStationNameFilterText(newfilter) {
    const stationName = document.getElementById('stationName');
    stationName.value = newfilter;
    stationName.dispatchEvent(new Event('change'));
}

// Hookup event listeners
(function () {
    const stationName = document.getElementById('stationName');
    stationName.addEventListener('change', function (e) {
        searchStore.filterStations(this.value);
    });
    stationName.addEventListener('paste', function (e) {
        searchStore.filterStations(this.value);
    });
    stationName.addEventListener('keyup', function (e) {
        searchStore.filterStations(this.value);
    });

    document.querySelectorAll('.btn').forEach(function (el) {
        el.addEventListener('click', function () {
            const currentText = stationName.value;
            changeStationNameFilterText(currentText + this.textContent);
        });
    });

    document.querySelector('.backspaceBtn').addEventListener('click', function () {
        const currentText = stationName.value;
        changeStationNameFilterText(currentText.substring(0, currentText.length - 1));
    });

    changeStationNameFilterText("");
})();