"use client";

import * as React from "react";
import { useShowAllDashboardData } from "@/services/api/dashboard";
import Avatar from "@mui/material/Avatar";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import LinearProgress from "@mui/material/LinearProgress";
import Stack from "@mui/material/Stack";
import type { SxProps } from "@mui/material/styles";
import Typography from "@mui/material/Typography";
import { ListBulletsIcon } from "@phosphor-icons/react/dist/ssr/ListBullets";

export interface TasksProgressProps {
	sx?: SxProps;
}

export function TasksProgress({ sx }: TasksProgressProps): React.JSX.Element {
	const { data: dashboardData, isError, isLoading } = useShowAllDashboardData();
	return (
		<Card sx={sx}>
			<CardContent>
				<Stack spacing={2}>
					<Stack direction="row" sx={{ alignItems: "flex-start", justifyContent: "space-between" }} spacing={3}>
						<Stack spacing={1}>
							<Typography color="text.secondary" gutterBottom variant="overline">
								Total Complete Bookings
							</Typography>
							<Typography variant="h4">{dashboardData?.data?.totalCompletedBookings}</Typography>
						</Stack>
						<Avatar sx={{ backgroundColor: "var(--mui-palette-warning-main)", height: "56px", width: "56px" }}>
							<ListBulletsIcon fontSize="var(--icon-fontSize-lg)" />
						</Avatar>
					</Stack>
					<div>
						<LinearProgress
							value={
								dashboardData?.data?.totalBookings
									? (dashboardData.data.totalCompletedBookings / dashboardData.data.totalBookings) * 100
									: 0
							}
							variant="determinate"
						/>
					</div>
				</Stack>
			</CardContent>
		</Card>
	);
}
