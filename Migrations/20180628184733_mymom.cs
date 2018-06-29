using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BeltExam.Migrations
{
    public partial class mymom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auction_users_UserId",
                table: "Auction");

            migrationBuilder.DropForeignKey(
                name: "FK_Bid_Auction_AuctionId",
                table: "Bid");

            migrationBuilder.DropForeignKey(
                name: "FK_Bid_users_UserId",
                table: "Bid");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bid",
                table: "Bid");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Auction",
                table: "Auction");

            migrationBuilder.RenameTable(
                name: "Bid",
                newName: "bids");

            migrationBuilder.RenameTable(
                name: "Auction",
                newName: "auctions");

            migrationBuilder.RenameIndex(
                name: "IX_Bid_UserId",
                table: "bids",
                newName: "IX_bids_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Bid_AuctionId",
                table: "bids",
                newName: "IX_bids_AuctionId");

            migrationBuilder.RenameIndex(
                name: "IX_Auction_UserId",
                table: "auctions",
                newName: "IX_auctions_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Product",
                table: "auctions",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "auctions",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_bids",
                table: "bids",
                column: "BidId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_auctions",
                table: "auctions",
                column: "AuctionId");

            migrationBuilder.AddForeignKey(
                name: "FK_auctions_users_UserId",
                table: "auctions",
                column: "UserId",
                principalTable: "users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bids_auctions_AuctionId",
                table: "bids",
                column: "AuctionId",
                principalTable: "auctions",
                principalColumn: "AuctionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bids_users_UserId",
                table: "bids",
                column: "UserId",
                principalTable: "users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_auctions_users_UserId",
                table: "auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_bids_auctions_AuctionId",
                table: "bids");

            migrationBuilder.DropForeignKey(
                name: "FK_bids_users_UserId",
                table: "bids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bids",
                table: "bids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_auctions",
                table: "auctions");

            migrationBuilder.RenameTable(
                name: "bids",
                newName: "Bid");

            migrationBuilder.RenameTable(
                name: "auctions",
                newName: "Auction");

            migrationBuilder.RenameIndex(
                name: "IX_bids_UserId",
                table: "Bid",
                newName: "IX_Bid_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_bids_AuctionId",
                table: "Bid",
                newName: "IX_Bid_AuctionId");

            migrationBuilder.RenameIndex(
                name: "IX_auctions_UserId",
                table: "Auction",
                newName: "IX_Auction_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Product",
                table: "Auction",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Auction",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bid",
                table: "Bid",
                column: "BidId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Auction",
                table: "Auction",
                column: "AuctionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auction_users_UserId",
                table: "Auction",
                column: "UserId",
                principalTable: "users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bid_Auction_AuctionId",
                table: "Bid",
                column: "AuctionId",
                principalTable: "Auction",
                principalColumn: "AuctionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bid_users_UserId",
                table: "Bid",
                column: "UserId",
                principalTable: "users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
