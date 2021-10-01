using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fysio.Migrations
{
    public partial class madepublic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BIGNumber",
                table: "Treators",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Treators",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentNumber",
                table: "Treators",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeacherNumber",
                table: "Treators",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Treatments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Treatments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Particularities",
                table: "Treatments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientFileId",
                table: "Treatments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Treatments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TreatmentDateTime",
                table: "Treatments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TreatorEmail",
                table: "Treatments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Treatments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Patients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegistrationNumber",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Student",
                table: "Patients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "PatientFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DiagnosisCode",
                table: "PatientFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiagnosisDescription",
                table: "PatientFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "PatientFiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "IntakerEmail",
                table: "PatientFiles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainTreatorEmail",
                table: "PatientFiles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "PatientFiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "PatientFiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SupervisingTreatorEmail",
                table: "PatientFiles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TreatmentPlanId",
                table: "PatientFiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "complaints",
                table: "PatientFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDateTime",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatorEmail",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientFileId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "VisibleForPatient",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FREndTime",
                table: "Availabilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FRStartTime",
                table: "Availabilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "MOEndTime",
                table: "Availabilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "MOStartTime",
                table: "Availabilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "THEndTime",
                table: "Availabilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "THStartTime",
                table: "Availabilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TUEndTime",
                table: "Availabilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TUStartTime",
                table: "Availabilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TreatorEmail",
                table: "Availabilities",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WEEndTime",
                table: "Availabilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "WEStartTime",
                table: "Availabilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDateTime",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TreatorEmail",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Accounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_PatientFileId",
                table: "Treatments",
                column: "PatientFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_PatientId",
                table: "Treatments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_TreatorEmail",
                table: "Treatments",
                column: "TreatorEmail");

            migrationBuilder.CreateIndex(
                name: "IX_PatientFiles_IntakerEmail",
                table: "PatientFiles",
                column: "IntakerEmail");

            migrationBuilder.CreateIndex(
                name: "IX_PatientFiles_MainTreatorEmail",
                table: "PatientFiles",
                column: "MainTreatorEmail");

            migrationBuilder.CreateIndex(
                name: "IX_PatientFiles_PatientId",
                table: "PatientFiles",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientFiles_SupervisingTreatorEmail",
                table: "PatientFiles",
                column: "SupervisingTreatorEmail");

            migrationBuilder.CreateIndex(
                name: "IX_PatientFiles_TreatmentPlanId",
                table: "PatientFiles",
                column: "TreatmentPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CreatorEmail",
                table: "Comments",
                column: "CreatorEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PatientFileId",
                table: "Comments",
                column: "PatientFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_TreatorEmail",
                table: "Availabilities",
                column: "TreatorEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_TreatorEmail",
                table: "Appointments",
                column: "TreatorEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PatientId",
                table: "Accounts",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Patients_PatientId",
                table: "Accounts",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Treators_TreatorEmail",
                table: "Appointments",
                column: "TreatorEmail",
                principalTable: "Treators",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Availabilities_Treators_TreatorEmail",
                table: "Availabilities",
                column: "TreatorEmail",
                principalTable: "Treators",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_PatientFiles_PatientFileId",
                table: "Comments",
                column: "PatientFileId",
                principalTable: "PatientFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Treators_CreatorEmail",
                table: "Comments",
                column: "CreatorEmail",
                principalTable: "Treators",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientFiles_Patients_PatientId",
                table: "PatientFiles",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientFiles_TreatmentPlans_TreatmentPlanId",
                table: "PatientFiles",
                column: "TreatmentPlanId",
                principalTable: "TreatmentPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientFiles_Treators_IntakerEmail",
                table: "PatientFiles",
                column: "IntakerEmail",
                principalTable: "Treators",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientFiles_Treators_MainTreatorEmail",
                table: "PatientFiles",
                column: "MainTreatorEmail",
                principalTable: "Treators",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientFiles_Treators_SupervisingTreatorEmail",
                table: "PatientFiles",
                column: "SupervisingTreatorEmail",
                principalTable: "Treators",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_PatientFiles_PatientFileId",
                table: "Treatments",
                column: "PatientFileId",
                principalTable: "PatientFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Patients_PatientId",
                table: "Treatments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Treators_TreatorEmail",
                table: "Treatments",
                column: "TreatorEmail",
                principalTable: "Treators",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Patients_PatientId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Treators_TreatorEmail",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Availabilities_Treators_TreatorEmail",
                table: "Availabilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_PatientFiles_PatientFileId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Treators_CreatorEmail",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientFiles_Patients_PatientId",
                table: "PatientFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientFiles_TreatmentPlans_TreatmentPlanId",
                table: "PatientFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientFiles_Treators_IntakerEmail",
                table: "PatientFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientFiles_Treators_MainTreatorEmail",
                table: "PatientFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientFiles_Treators_SupervisingTreatorEmail",
                table: "PatientFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_PatientFiles_PatientFileId",
                table: "Treatments");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Patients_PatientId",
                table: "Treatments");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Treators_TreatorEmail",
                table: "Treatments");

            migrationBuilder.DropIndex(
                name: "IX_Treatments_PatientFileId",
                table: "Treatments");

            migrationBuilder.DropIndex(
                name: "IX_Treatments_PatientId",
                table: "Treatments");

            migrationBuilder.DropIndex(
                name: "IX_Treatments_TreatorEmail",
                table: "Treatments");

            migrationBuilder.DropIndex(
                name: "IX_PatientFiles_IntakerEmail",
                table: "PatientFiles");

            migrationBuilder.DropIndex(
                name: "IX_PatientFiles_MainTreatorEmail",
                table: "PatientFiles");

            migrationBuilder.DropIndex(
                name: "IX_PatientFiles_PatientId",
                table: "PatientFiles");

            migrationBuilder.DropIndex(
                name: "IX_PatientFiles_SupervisingTreatorEmail",
                table: "PatientFiles");

            migrationBuilder.DropIndex(
                name: "IX_PatientFiles_TreatmentPlanId",
                table: "PatientFiles");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CreatorEmail",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_PatientFileId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Availabilities_TreatorEmail",
                table: "Availabilities");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_TreatorEmail",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_PatientId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "BIGNumber",
                table: "Treators");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Treators");

            migrationBuilder.DropColumn(
                name: "StudentNumber",
                table: "Treators");

            migrationBuilder.DropColumn(
                name: "TeacherNumber",
                table: "Treators");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "Particularities",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "PatientFileId",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "TreatmentDateTime",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "TreatorEmail",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "RegistrationNumber",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Student",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "PatientFiles");

            migrationBuilder.DropColumn(
                name: "DiagnosisCode",
                table: "PatientFiles");

            migrationBuilder.DropColumn(
                name: "DiagnosisDescription",
                table: "PatientFiles");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "PatientFiles");

            migrationBuilder.DropColumn(
                name: "IntakerEmail",
                table: "PatientFiles");

            migrationBuilder.DropColumn(
                name: "MainTreatorEmail",
                table: "PatientFiles");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "PatientFiles");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "PatientFiles");

            migrationBuilder.DropColumn(
                name: "SupervisingTreatorEmail",
                table: "PatientFiles");

            migrationBuilder.DropColumn(
                name: "TreatmentPlanId",
                table: "PatientFiles");

            migrationBuilder.DropColumn(
                name: "complaints",
                table: "PatientFiles");

            migrationBuilder.DropColumn(
                name: "CreationDateTime",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CreatorEmail",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "PatientFileId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "VisibleForPatient",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "FREndTime",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "FRStartTime",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "MOEndTime",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "MOStartTime",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "THEndTime",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "THStartTime",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "TUEndTime",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "TUStartTime",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "TreatorEmail",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "WEEndTime",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "WEStartTime",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "AppointmentDateTime",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "TreatorEmail",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Accounts");
        }
    }
}
