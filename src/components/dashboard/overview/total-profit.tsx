'use client'
import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Stack from '@mui/material/Stack';
import type { SxProps } from '@mui/material/styles';
import Typography from '@mui/material/Typography';
import { ReceiptIcon } from '@phosphor-icons/react/dist/ssr/Receipt';
import { useShowAllDashboardData } from '@/services/api/dashboard';
import { ArrowUpIcon } from '@phosphor-icons/react/dist/ssr/ArrowUp';
import { UsersIcon } from '@phosphor-icons/react/dist/ssr/Users';

export interface TotalProfitProps {
  sx?: SxProps;
  value: string;
  diff?: number;
}

export function TotalProfit({ diff,value, sx }: TotalProfitProps): React.JSX.Element {
  const TrendIcon =  ArrowUpIcon 
  const trendColor =  'var(--mui-palette-success-main)' 
    const { data: dashboardData, isError, isLoading } = useShowAllDashboardData();
  return (
    <Card sx={sx}>
      <CardContent>
        <Stack direction="row" sx={{ alignItems: 'flex-start', justifyContent: 'space-between' }} spacing={3}>
          <Stack spacing={1}>
            <Typography color="text.secondary" variant="overline">
              Total Technicians
            </Typography>
            <Typography variant="h4">{dashboardData?.data?.totalTechnicians}</Typography>
          </Stack>
          <Avatar sx={{ backgroundColor: 'var(--mui-palette-primary-main)', height: '56px', width: '56px' }}>
            {/* <ReceiptIcon fontSize="var(--icon-fontSize-lg)" /> */}
            <UsersIcon fontSize="var(--icon-fontSize-lg)" />
          </Avatar>
        </Stack>
         
            <Stack sx={{ alignItems: 'center' }} direction="row" spacing={2} marginTop={2}>
              <Stack sx={{ alignItems: 'center' }} direction="row" spacing={0.5}>
                <TrendIcon color={trendColor} fontSize="var(--icon-fontSize-md)" />
                <Typography color={trendColor} variant="body2">
                  5%
                </Typography>
              </Stack>
              <Typography color="text.secondary" variant="caption">
                Since last month
              </Typography>
            </Stack>
          
      </CardContent>
    </Card>
  );
}
