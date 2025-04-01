let skipassIndex = 0;
let equipmentIndex = 0;
let accommodationIndex = 0;

function createCard(content) {
    const div = document.createElement("div");
    div.className = "border p-4 rounded shadow-sm mb-4 bg-white";
    div.innerHTML = content;
    return div;
}

function addSkiPass() {
    let html = ``;

    if (window.skiPasses.length > 0) {
        html += `<input type="hidden" name="SkiPassSelections[${skipassIndex}].SkiPassId" value="${window.skiPasses[0].Value}" />`;
    }

    html += `<div class='mb-3'><label class='form-label'>SkiPass Type:</label>
        <select name="SkiPassSelections[${skipassIndex}].SkiPassTypeId" class="form-select">`;
    window.skiPassTypes.forEach(tp => html += `<option value='${tp.Value}'>${tp.Text}</option>`);
    html += `</select></div>`;

    html += `<div class='mb-3'><label class='form-label'>Days:</label>
        <input type="number" name="SkiPassSelections[${skipassIndex}].Days" class="form-control" min="1" required /></div>`;

    html += `<div class='mb-3'><label class='form-label'>Quantity:</label>
        <input type="number" name="SkiPassSelections[${skipassIndex}].Quantity" class="form-control" min="1" required /></div>`;

    document.getElementById("skiPassContainer").appendChild(createCard(html));
    skipassIndex++;
}

function addEquipment() {
    let html = `<div class='mb-3'><label class='form-label'>Equipment:</label>
        <select name="EquipmentSelections[${equipmentIndex}].EquipmentRentalId" class="form-select">`;
    window.equipmentOptions.forEach(eq => html += `<option value='${eq.Value}'>${eq.Text}</option>`);
    html += `</select></div>`;

    html += `<div class='mb-3'><label class='form-label'>Days:</label>
        <input type="number" name="EquipmentSelections[${equipmentIndex}].Days" class="form-control" min="1" required /></div>`;

    html += `<div class='mb-3'><label class='form-label'>Quantity:</label>
        <input type="number" name="EquipmentSelections[${equipmentIndex}].Quantity" class="form-control" min="1" required /></div>`;

    document.getElementById("equipmentContainer").appendChild(createCard(html));
    equipmentIndex++;
}

function addAccommodation() {
    let html = `<div class='mb-3'><label class='form-label'>Accommodation:</label>
        <select name="AccommodationSelections[${accommodationIndex}].AccommodationId" class="form-select">`;
    window.accommodationOptions.forEach(acc => html += `<option value='${acc.Value}'>${acc.Text}</option>`);
    html += `</select></div>`;

    html += `<div class='mb-3'><label class='form-label'>Start Date:</label>
        <input type="text" id="accStart${accommodationIndex}" name="AccommodationSelections[${accommodationIndex}].StartDate" class="form-control" required /></div>`;

    html += `<div class='mb-3'><label class='form-label'>End Date:</label>
        <input type="text" id="accEnd${accommodationIndex}" name="AccommodationSelections[${accommodationIndex}].EndDate" class="form-control" required /></div>`;

    html += `<div class='mb-3'><label class='form-label'>Guests:</label>
        <input type="number" name="AccommodationSelections[${accommodationIndex}].Guests" class="form-control" min="1" required /></div>`;

    document.getElementById("accommodationContainer").appendChild(createCard(html));

    const startInput = document.getElementById(`accStart${accommodationIndex}`);
    const endInput = document.getElementById(`accEnd${accommodationIndex}`);

    const startPicker = new Litepicker({
        element: startInput,
        format: 'YYYY-MM-DD',
        minDate: new Date(),
        singleMode: true,
        setup: (picker) => {
            picker.on('selected', (date) => {
                if (endInput._litepicker) {
                    endInput._litepicker.setOptions({ minDate: date.format('YYYY-MM-DD') });
                }
            });
        }
    });

    const endPicker = new Litepicker({
        element: endInput,
        format: 'YYYY-MM-DD',
        minDate: new Date(),
        singleMode: true
    });

    startInput._litepicker = startPicker;
    endInput._litepicker = endPicker;

    accommodationIndex++;
}

window.onload = function () {
    addSkiPass();
    addEquipment();
    addAccommodation();
};
