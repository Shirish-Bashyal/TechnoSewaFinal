"use client";

import * as React from "react";
import { useShowAllBookingData } from "@/services/api/booking";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Card from "@mui/material/Card";
import CardActions from "@mui/material/CardActions";
import CardHeader from "@mui/material/CardHeader";
import Chip from "@mui/material/Chip";
import Divider from "@mui/material/Divider";
import type { SxProps } from "@mui/material/styles";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import { ArrowRightIcon } from "@phosphor-icons/react/dist/ssr/ArrowRight";
import dayjs from "dayjs";

const statusMap = {
	pending: { label: "Pending", color: "error" },
	completed: { label: "Completed", color: "success" },
	booked: { label: "Booked", color: "warning" },
} as const;

export interface Order {
	id: string;
	customer: { name: string };
	amount: number;
	status: "pending" | "delivered" | "refunded";
	createdAt: Date;
}

export interface LatestOrdersProps {
	orders?: Order[];
	sx?: SxProps;
}

export function LatestOrders({ orders = [], sx }: LatestOrdersProps): React.JSX.Element {
	const { data: bookingData, isError, isLoading } = useShowAllBookingData();
	return (
		<Card sx={sx}>
			<CardHeader title="Latest Bookings" />
			<Divider />
			<Box sx={{ overflowX: "auto" }}>
				<Table sx={{ minWidth: 800 }}>
					<TableHead>
						<TableRow>
							<TableCell>Order</TableCell>
							<TableCell>Consumer</TableCell>
							<TableCell>Technician</TableCell>
							<TableCell sortDirection="desc">Date</TableCell>
							<TableCell>Status</TableCell>
						</TableRow>
					</TableHead>
					<TableBody>
						{bookingData?.data?.map((booking: any, index: number) => {
							const normalizedStatus = booking.status?.toLowerCase(); // "Completed" => "completed"
							const { label, color } = statusMap[normalizedStatus as keyof typeof statusMap] ?? {
								label: "Unknown",
								color: "default",
							};

							return (
								<TableRow hover key={booking.id}>
									<TableCell>{booking.id}</TableCell>
									<TableCell>{booking.consumerName}</TableCell>
									<TableCell>{booking.technicianName}</TableCell>
									<TableCell>{booking.serviceDate}</TableCell>
									<TableCell>
										<Chip color={color} label={label} size="small" />
									</TableCell>
								</TableRow>
							);
						})}
					</TableBody>
				</Table>
			</Box>
			<Divider />
			<CardActions sx={{ justifyContent: "flex-end" }}>
				<Button
					color="inherit"
					endIcon={<ArrowRightIcon fontSize="var(--icon-fontSize-md)" />}
					size="small"
					variant="text"
				>
					View all
				</Button>
			</CardActions>
		</Card>
	);
}
