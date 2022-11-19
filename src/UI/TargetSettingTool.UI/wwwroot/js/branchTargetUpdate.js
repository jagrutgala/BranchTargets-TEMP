const makeDropDownUsingAjax = (url, htmlId, foreachCallback) => {
  $.ajax({
    type: "GET",
    url: `${url}`,
    dataType: "json",
    contentType: "application/json",
    success: function (result) {
      console.table(result);
      $(`#${htmlId}`)
        .empty()
        .append(
          `<option data-default-option value="">Select a option</option>`
        );
      $.each(result, foreachCallback);
    },
    error: function (err) {
      console.log(err);
    },
  });
};

makeDropDownUsingAjax(
  "/UserBranch/GetBranchesByUserIdJson?userId=85FE2CE2-FB73-4457-1ACA-08DACA3249EA",
  "BranchId",
  (indx, value) => {
    $("#BranchId").append(
      $("<option></option>").val(value.branchId).html(value.branch.name)
    );
  }
);

$("#BranchId").on("change", (event) => {
  $("#StartDate").prop("disabled", false);
  makeDropDownUsingAjax(
    "/UserParameter/GetUserParametersByBranchIdJson?branchId=46C7032C-5D7A-4BB3-2A37-08DAC6ECC2FE",
    "StartDate",
    (indx, value) => {
      // date formating here
      let dateString = ``;
      $("#StartDate").append(
        $("<option></option>")
          .val(value.parameter.startDate)
          .html(`${value.parameter.startDate} - ${value.parameter.endDate}`)
      );
    }
  );
});

function addBranchTargets() {
  let newTargetString = `
                    <tr>
                        <td data-parameter></td>
                        <td data-target></td>
                        <td action>
                          <button type="button">
                              <i class="fa-solid fa-trash"></i>
                          </button>
                        </td>
                    </tr>
                `
  let newTarget = $(newTargetString);

  const parameterSelect = $("#ParameterType");
  const targetAmountInput = $("#TargetAmount");
  const paramId = parameterSelect.find(":selected").val();
  const paramName = parameterSelect.find(":selected").text();
  const targetAmt = targetAmountInput.val();

  newTarget.addClass("target");
  newTarget.addClass(`parameter-${paramId}`);

  newTarget.find("td[data-parameter]").text(`${paramName}`);
  newTarget.find("td[data-parameter]").data("data-parameter", `${paramId}`);
  newTarget.find("td[data-target]").text(`${targetAmt}`);
  newTarget.find("td[data-target]").data("data-target", `${targetAmt}`);
  newTarget.find("td[data-target]").text(`${targetAmt}`);

  newTarget.find("button").addClass("btn");
  newTarget.find("button").on("click", (event) => {
    parameterSelect.find(`.${paramId}`).removeClass("hide");
    newTarget.remove();
    if (document.querySelector("#TargetList").childElementCount <= 0) {
      document.querySelector("#StartDate").readOnly = false;
      document.querySelector("#EndDate").readOnly = false;
    }
  });

  parameterSelect.find(`[data-default-option]`).prop("selected", true);
  parameterSelect.find(`.${paramId}`).addClass("hide");
  targetAmountInput.val("");

  newTarget.appendTo("#TargetList");

  if (document.querySelector("#TargetList").childElementCount > 0) {
    document.querySelector("#StartDate").readOnly = true;
    document.querySelector("#EndDate").readOnly = true;
  }
}

$("#StartDate").on("change", (event) => {
  let branchId = $("#BranchId").val();
  let startDate = $("#StartDate").val();
  $.ajax({
    type: "GET",
    url: `/BranchTarget/GetAllTargetsJson?branchId=${branchId}&startDate=${startDate}`,
    dataType: "json",
    contentType: "application/json",
    success: function (result) {
      console.log("success");
      console.table(result);

      $("#BranchTargets").append();
    },
    error: function (err) {
      console.log(err);
    },
  });
});

// makeDropDownUsingAjax()

// $.ajax({
//   url: "/UserBranch/GetBranchesByUserIdJson?userId=85FE2CE2-FB73-4457-1ACA-08DACA3249EA",
//   type: "GET",
//   success: function (res) {
//     console.table(res);
//     $.each(d, function (i, para) {
//       $("#ParameterType").append(
//         $("<option></option>")
//           .addClass(`${para.id}`)
//           .val(para.id)
//           .html(para.name)
//       );
//     });
//   },
//   error: function () {
//     alert("Error!");
//   },
// })
