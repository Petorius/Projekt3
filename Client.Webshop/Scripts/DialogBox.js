function ConfirmDelete() {
    var x = confirm("Er du sikker på du vil slette din bruger?");
    if (x) {
        return true
    }
    else {
        return false
    }
}
