"use client";

import * as React from "react";
import { useShowAllTechnicianData } from "@/services/api/technician";
import { useVerifyTechnicianMutation } from "@/services/api/verfiytechnician";
import Avatar from "@mui/material/Avatar";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Card from "@mui/material/Card";
import Checkbox from "@mui/material/Checkbox";
import Chip from "@mui/material/Chip";
import Divider from "@mui/material/Divider";
import Stack from "@mui/material/Stack";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableHead from "@mui/material/TableHead";
import TablePagination from "@mui/material/TablePagination";
import TableRow from "@mui/material/TableRow";
import Typography from "@mui/material/Typography";
import dayjs from "dayjs";

import { useSelection } from "@/hooks/use-selection";

function noop(): void {
	// do nothing
}

export interface Customer {
	id: string;
	avatar: string;
	name: string;
	email: string;
	address: { city: string; state: string; country: string; street: string };
	phone: string;
	createdAt: Date;
}

interface TechnicianTableProps {
	count?: number;
	page?: number;
	rows?: Customer[];
	rowsPerPage?: number;
}

const statusMap = {
	false: { label: "false", color: "error" },
	true: { label: "true", color: "success" },
} as const;

export function TechnicianTable({
	count = 0,
	rows = [],
	page = 0,
	rowsPerPage = 0,
}: TechnicianTableProps): React.JSX.Element {
	const rowIds = React.useMemo(() => {
		return rows.map((customer) => customer.id);
	}, [rows]);

	const { selectAll, deselectAll, selectOne, deselectOne, selected } = useSelection(rowIds);

	const selectedSome = (selected?.size ?? 0) > 0 && (selected?.size ?? 0) < rows.length;
	const selectedAll = rows.length > 0 && selected?.size === rows.length;
	const { data: technicianData, isError, isLoading } = useShowAllTechnicianData();
	const { mutate: verifyTechnicianMutation, isPending } = useVerifyTechnicianMutation();
	const [verifyingId, setVerifyingId] = React.useState<number | null>(null);

	return (
		<Card>
			<Box sx={{ overflowX: "auto" }}>
				<Table sx={{ minWidth: "800px" }}>
					<TableHead>
						<TableRow>
							<TableCell padding="checkbox">
								<Checkbox
									checked={selectedAll}
									indeterminate={selectedSome}
									onChange={(event) => {
										if (event.target.checked) {
											selectAll();
										} else {
											deselectAll();
										}
									}}
								/>
							</TableCell>
							<TableCell>Name</TableCell>
							<TableCell>Address</TableCell>
							<TableCell>Phone Number</TableCell>
							<TableCell>Second Number</TableCell>
							<TableCell>Verfied Status</TableCell>
							<TableCell>Actions</TableCell>
						</TableRow>
					</TableHead>
					<TableBody>
						{technicianData?.data?.map((tech: any, index: number) => {
							const isSelected = selected?.has(tech.technicianId);
							const statusKey = String(tech.isVerified) as "true" | "false";
							const { label, color } = statusMap[statusKey];
							return (
								<TableRow hover key={tech.technicianId} selected={isSelected}>
									<TableCell padding="checkbox">
										<Checkbox
											checked={isSelected}
											onChange={(event) => {
												if (event.target.checked) {
													selectOne(tech.technicianId);
												} else {
													deselectOne(tech.technicianId);
												}
											}}
										/>
									</TableCell>
									<TableCell>
										<Stack sx={{ alignItems: "center" }} direction="row" spacing={2}>
											{/* <Avatar src={row.avatar} /> */}
											<Typography variant="subtitle2">{tech.name}</Typography>
										</Stack>
									</TableCell>
									<TableCell>{tech.address}</TableCell>
									<TableCell>{tech.phoneNumber}</TableCell>
									<TableCell>{tech.secondPhoneNumber}</TableCell>
									<TableCell>
										<Chip color={color} label={label} size="small" />
									</TableCell>
									<TableCell>
										<Button
											variant="contained"
											color="primary"
											size="small"
											disabled={tech.isVerified || verifyingId === tech.technicianId}
											onClick={() => {
												setVerifyingId(tech.technicianId);
												verifyTechnicianMutation(tech.technicianId, {
													onSuccess: () => {
														setVerifyingId(null); // Reset after success
													},
													onError: () => {
														setVerifyingId(null); // Reset on error too
													},
												});
											}}
										>
											{tech.isVerified ? "Verified" : verifyingId === tech.technicianId ? "Verifying..." : "Verify"}
										</Button>
									</TableCell>
								</TableRow>
							);
						})}
					</TableBody>
				</Table>
			</Box>
			<Divider />
			<TablePagination
				component="div"
				count={count}
				onPageChange={noop}
				onRowsPerPageChange={noop}
				page={page}
				rowsPerPage={rowsPerPage}
				rowsPerPageOptions={[5, 10, 25]}
			/>
		</Card>
	);
}
