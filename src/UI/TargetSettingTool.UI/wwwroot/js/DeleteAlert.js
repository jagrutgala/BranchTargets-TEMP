function DeleteAlert(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.assign(url);
        }
    })
}

//Delete Alert For Right
function DeleteRight(id) {
    DeleteAlert(`/Right/DeleteRight/${id}`);
}

//Delete Alert For Role
function DeleteRole(id) {
     DeleteAlert(`/Role/DeleteRole/${id}`);
}

//Delete Alert For ParameterType
function DeleteParameterType(id) {
    DeleteAlert(`/ParameterType/DeleteParameterType/${id}`);
}
           
//Delete Alert For Parameter
function DeleteParameter(id) {
    DeleteAlert(`/Parameter/DeleteParameter/${id}`);
}


//Delete Alert For Branch
function DeleteBranch(id) {
    DeleteAlert(`/Branch/RemoveBranch/${id}`);
}


