using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CNPM_QBCA.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskTypeToAssignedPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedPlan_ExamPlanDistributions_DistributionID",
                table: "AssignedPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignedPlan_ExamPlans_ExamPlanID",
                table: "AssignedPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignedPlan_Users_AssignedByID",
                table: "AssignedPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignedPlan_Users_AssignedToID",
                table: "AssignedPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionApproval_SubmissionTables_SubmissionTableID",
                table: "SubmissionApproval");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionApproval_Users_ApprovedBy",
                table: "SubmissionApproval");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubmissionApproval",
                table: "SubmissionApproval");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignedPlan",
                table: "AssignedPlan");

            migrationBuilder.RenameTable(
                name: "SubmissionApproval",
                newName: "SubmissionApprovals");

            migrationBuilder.RenameTable(
                name: "AssignedPlan",
                newName: "AssignPlans");

            migrationBuilder.RenameIndex(
                name: "IX_SubmissionApproval_SubmissionTableID",
                table: "SubmissionApprovals",
                newName: "IX_SubmissionApprovals_SubmissionTableID");

            migrationBuilder.RenameIndex(
                name: "IX_SubmissionApproval_ApprovedBy",
                table: "SubmissionApprovals",
                newName: "IX_SubmissionApprovals_ApprovedBy");

            migrationBuilder.RenameIndex(
                name: "IX_AssignedPlan_ExamPlanID",
                table: "AssignPlans",
                newName: "IX_AssignPlans_ExamPlanID");

            migrationBuilder.RenameIndex(
                name: "IX_AssignedPlan_DistributionID",
                table: "AssignPlans",
                newName: "IX_AssignPlans_DistributionID");

            migrationBuilder.RenameIndex(
                name: "IX_AssignedPlan_AssignedToID",
                table: "AssignPlans",
                newName: "IX_AssignPlans_AssignedToID");

            migrationBuilder.RenameIndex(
                name: "IX_AssignedPlan_AssignedByID",
                table: "AssignPlans",
                newName: "IX_AssignPlans_AssignedByID");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "SubmissionTables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubmissionApprovals",
                table: "SubmissionApprovals",
                column: "SubmissionApprovalID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignPlans",
                table: "AssignPlans",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignPlans_ExamPlanDistributions_DistributionID",
                table: "AssignPlans",
                column: "DistributionID",
                principalTable: "ExamPlanDistributions",
                principalColumn: "DistributionID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignPlans_ExamPlans_ExamPlanID",
                table: "AssignPlans",
                column: "ExamPlanID",
                principalTable: "ExamPlans",
                principalColumn: "ExamPlanID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignPlans_Users_AssignedByID",
                table: "AssignPlans",
                column: "AssignedByID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignPlans_Users_AssignedToID",
                table: "AssignPlans",
                column: "AssignedToID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionApprovals_SubmissionTables_SubmissionTableID",
                table: "SubmissionApprovals",
                column: "SubmissionTableID",
                principalTable: "SubmissionTables",
                principalColumn: "SubmissionID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionApprovals_Users_ApprovedBy",
                table: "SubmissionApprovals",
                column: "ApprovedBy",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignPlans_ExamPlanDistributions_DistributionID",
                table: "AssignPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignPlans_ExamPlans_ExamPlanID",
                table: "AssignPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignPlans_Users_AssignedByID",
                table: "AssignPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignPlans_Users_AssignedToID",
                table: "AssignPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionApprovals_SubmissionTables_SubmissionTableID",
                table: "SubmissionApprovals");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionApprovals_Users_ApprovedBy",
                table: "SubmissionApprovals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubmissionApprovals",
                table: "SubmissionApprovals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignPlans",
                table: "AssignPlans");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SubmissionTables");

            migrationBuilder.RenameTable(
                name: "SubmissionApprovals",
                newName: "SubmissionApproval");

            migrationBuilder.RenameTable(
                name: "AssignPlans",
                newName: "AssignedPlan");

            migrationBuilder.RenameIndex(
                name: "IX_SubmissionApprovals_SubmissionTableID",
                table: "SubmissionApproval",
                newName: "IX_SubmissionApproval_SubmissionTableID");

            migrationBuilder.RenameIndex(
                name: "IX_SubmissionApprovals_ApprovedBy",
                table: "SubmissionApproval",
                newName: "IX_SubmissionApproval_ApprovedBy");

            migrationBuilder.RenameIndex(
                name: "IX_AssignPlans_ExamPlanID",
                table: "AssignedPlan",
                newName: "IX_AssignedPlan_ExamPlanID");

            migrationBuilder.RenameIndex(
                name: "IX_AssignPlans_DistributionID",
                table: "AssignedPlan",
                newName: "IX_AssignedPlan_DistributionID");

            migrationBuilder.RenameIndex(
                name: "IX_AssignPlans_AssignedToID",
                table: "AssignedPlan",
                newName: "IX_AssignedPlan_AssignedToID");

            migrationBuilder.RenameIndex(
                name: "IX_AssignPlans_AssignedByID",
                table: "AssignedPlan",
                newName: "IX_AssignedPlan_AssignedByID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubmissionApproval",
                table: "SubmissionApproval",
                column: "SubmissionApprovalID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignedPlan",
                table: "AssignedPlan",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedPlan_ExamPlanDistributions_DistributionID",
                table: "AssignedPlan",
                column: "DistributionID",
                principalTable: "ExamPlanDistributions",
                principalColumn: "DistributionID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedPlan_ExamPlans_ExamPlanID",
                table: "AssignedPlan",
                column: "ExamPlanID",
                principalTable: "ExamPlans",
                principalColumn: "ExamPlanID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedPlan_Users_AssignedByID",
                table: "AssignedPlan",
                column: "AssignedByID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedPlan_Users_AssignedToID",
                table: "AssignedPlan",
                column: "AssignedToID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionApproval_SubmissionTables_SubmissionTableID",
                table: "SubmissionApproval",
                column: "SubmissionTableID",
                principalTable: "SubmissionTables",
                principalColumn: "SubmissionID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionApproval_Users_ApprovedBy",
                table: "SubmissionApproval",
                column: "ApprovedBy",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
