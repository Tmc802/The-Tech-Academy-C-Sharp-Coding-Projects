function btn_count_entries() {
    var e = document.getElementById("countDropdown");
    var strUser = e.options[e.selectedIndex];
    document.getElementById("countDisplay").innerHTML = strUser.innerHTML;
}